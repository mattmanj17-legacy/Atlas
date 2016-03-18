// 1 : print(char* c) --- prints a null terminated string to the console, and a newline;
void print(char* toPrint)
{
	asm("SYSCALL 1");
	return;
}

// 2 : printInt(int c) --- prints a number to the console and a newline;
void printInt(int toPrint)
{
	asm("SYSCALL 2");
	return;
}

//3 : clear screen() -- clears screen to black
void clearScreen()
{
	asm("SYSCALL 3");
	return;
}

//4 : setPix(int x, int y, int r, int g, int b) -- screen[x,y] = color(r,g,b)
void setPix(int x, int y, int r, int g, int b)
{
	asm("SYSCALL 4");
	return;
}

//5 : swapBuffers
void SwapBuffers()
{
	asm("SYSCALL 5");
	return;
}

//-------------------------------------------

int abs(int x)
{
	return x < 0 ? -x : x;
}

void line(int x, int y, int x2, int y2) 
{
	int w = x2 - x;
	int h = y2 - y;
	
	int dx1 = 0;
	int dy1 = 0;
	int dx2 = 0; 
	int dy2 = 0;
	
	if (w < 0) dx1 = -1; 
	else if (w>0) dx1 = 1;

	if (h < 0) dy1 = -1; 
	else if (h>0) dy1 = 1;

	if (w < 0) dx2 = -1; 
	else if (w>0) dx2 = 1;
	
	int longest = abs(w);
	int shortest = abs(h);

	if (!(longest > shortest)) 
	{
		longest = abs(h);
		shortest = abs(w);

		if (h < 0) dy2 = -1; 
		else if (h>0) dy2 = 1;

		dx2 = 0;
	}

	int numerator = longest >> 1;

	int i = 0;

	while (i <= longest)
	{
		(setPix(x, y, 255, 255, 255));
		numerator += shortest;
		if (!(numerator < longest))
		{
			numerator = numerator - longest;
			x += dx1;
			y += dy1;
		}
		else
		{
			x += dx2;
			y += dy2;
		}

		//(print("line"));

		i++;
	}
	return;
}

int main()
{
	int sideWidth = 130;
	
	int x0 = 320;
	int y0 = 170;

	int x1 = x0 + sideWidth;
	int y1 = y0 + sideWidth;

	int d = 5;

	int state = 1;

	while (1)
	{
		(clearScreen());

		//todo ugh args are passed backwards
		line(y1, x1, y0, x0);

		//(print("loop"));

		if (state == 0)
		{
			x1 += d;

			if (x1 > x0 + sideWidth)
			{
				state = 1;
			}
		}
		else if (state == 1)
		{
			y1 = y1 - d;

			if (y1 < y0 - sideWidth)
			{
				state = 2;
			}
		}
		else if (state == 2)
		{
			x1 = x1 - d;
			if (x1 < x0 - sideWidth)
			{
				state = 3;
			}
		}
		else
		{
			y1 += d;
			if (y1 > y0 + sideWidth)
			{
				state = 0;
			}
		}

		(SwapBuffers());
	}
	return;
}