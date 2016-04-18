using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Atlas.Architecture;
using Atlas.Assembler;
using Atlas.AtlasCC;
using Antlr4.Runtime;
using System.Threading;

using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace Atlas.CLI
{
    class VirtualAtlasCPU : AtlasCPU
    {
        static VirtualAtlasCPU cpu;
        static Thread cpuThread;
        static int iTexture = 0;

        static Bitmap bmpBG;

        int lastinsts = 0;

        static Stopwatch watch;

        static void InitCPU(string[] args)
        {
            if(args[0] == "-c")
            {
                AtlasCCompilerFrontEnd compiler = new AtlasCCompilerFrontEnd();

                string source = File.ReadAllText(args[1]);

                string compiled = compiler.Compile(new AntlrInputStream(source));

                if (compiled != null)
                {
                    File.WriteAllText(args[2], compiled);
                }
                Environment.Exit(0);
            }
            else if(args[0] == "-a")
            {
                AtlasAssembler assembler = new AtlasAssembler();

                string compiled = File.ReadAllText(args[1]);

                byte[] program = assembler.Assemble(new AntlrInputStream(compiled));

                if (program != null)
                {
                    File.WriteAllBytes(args[2], program);
                }
                Environment.Exit(0);
            }
            else if(args[0] == "-e")
            {
                cpu = new VirtualAtlasCPU();

                byte[] program = File.ReadAllBytes(args[1]);

                cpu.LoadProgram(program);

                Action cpuMain = () =>
                {
                    while (true)
                    {
                        cpu.ClockPulse();
                    }
                };

                cpuThread = new Thread(() => cpuMain());

                Console.Clear();
            } 
        }

        static Action UpdateTexture;
        private static bool needUpdateBuffer;
        private long lastMili = 0;

        static void Main(string[] args)
        {
            InitCPU(args);

            bmpBG = new Bitmap(640, 340);
            using (var g = Graphics.FromImage(bmpBG))
            {
                g.Clear(Color.Red);
            }

            using (var game = new GameWindow())
            {
                game.Load += (sender, e) =>
                {
                    // setup settings, load textures, sounds
                    game.VSync = VSyncMode.On;

                    game.Size = new Size(640, 340);


                    GL.Enable(EnableCap.Texture2D);
                    GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
                    GL.GenTextures(1, out iTexture);
                    GL.BindTexture(TextureTarget.Texture2D, iTexture);

                    BitmapData data = bmpBG.LockBits(new System.Drawing.Rectangle(0, 0, bmpBG.Width, bmpBG.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                        OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

                    bmpBG.UnlockBits(data);

                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

                    GL.BindTexture(TextureTarget.Texture2D, 0);

                    GL.FrontFace(FrontFaceDirection.Ccw);

                    cpuThread.Start();

                    watch = Stopwatch.StartNew();

                };

                game.Resize += (sender, e) =>
                {
                    GL.Viewport(0, 0, game.Width, game.Height);
                };

                game.UpdateFrame += (sender, e) =>
                {
                    // add input handling
                    if (game.Keyboard[Key.Escape])
                    {
                        game.Exit();
                    }
                };

                game.RenderFrame += (sender, e) =>
                {
                    // render graphics
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                    GL.MatrixMode(MatrixMode.Projection);
                    GL.LoadIdentity();
                    GL.Ortho(-1.0, 1.0, -1.0, 1.0, -1, 1);

                    lock (UpdateTexture)
                    {
                        if (needUpdateBuffer)
                        {
                            BitmapData data = bmpBG.LockBits(new System.Drawing.Rectangle(0, 0, bmpBG.Width, bmpBG.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                            GL.BindTexture(TextureTarget.Texture2D, iTexture);
                            GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, data.Width, data.Height, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

                            GL.BindTexture(TextureTarget.Texture2D, 0);

                            bmpBG.UnlockBits(data);
                            needUpdateBuffer = false;
                        }
                    }

                    GL.BindTexture(TextureTarget.Texture2D, iTexture);

                    GL.Begin(PrimitiveType.Triangles);

                    GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(-1.0f, 1.0f);
                    GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(1.0f, 1.0f);
                    GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(-1.0f, -1.0f);

                    GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(-1.0f, -1.0f);
                    GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(1.0f, 1.0f);
                    GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(1.0f, -1.0f);

                    GL.End();

                    GL.BindTexture(TextureTarget.Texture2D, 0);

                    game.SwapBuffers();
                };

                UpdateTexture = () =>
                {
                    lock(UpdateTexture)
                    {
                        needUpdateBuffer = true;
                    }
                    while (needUpdateBuffer) { }
                };

                // Run the game at 60 updates per second
                game.Run(60.0);
            }
        }

        protected override void SysPrint(int charAddr)
        {
            char c = (char)MemValue(MemSize.BYTE, charAddr);
            while (c != '\0')
            {
                Console.Write(c);
                charAddr++;
                c = (char)MemValue(MemSize.BYTE, charAddr);
            }
            Console.WriteLine();
        }

        protected override void SysPrintInt(int toPrint)
        {
            Console.WriteLine(toPrint);
        }

        protected override void SysClearSreen()
        {
            using (var g = Graphics.FromImage(bmpBG))
            {
                g.Clear(Color.Black);
            }
        }

        protected override void SysSetPix(int x, int y, int r, int g, int b)
        {
            bmpBG.SetPixel(x, y, Color.FromArgb(r, g, b));
        }

        protected override void SysSwapBuffer()
        {
            Console.WriteLine("mili: " + (watch.ElapsedMilliseconds - lastMili) + " insts: " + (insts - lastinsts) + " ratio (inst/sec): " + ((insts - lastinsts) / ((watch.ElapsedMilliseconds - lastMili)/1000.0)));
            lastMili = watch.ElapsedMilliseconds;
            lastinsts = insts;
            UpdateTexture();
        }
    }
}
