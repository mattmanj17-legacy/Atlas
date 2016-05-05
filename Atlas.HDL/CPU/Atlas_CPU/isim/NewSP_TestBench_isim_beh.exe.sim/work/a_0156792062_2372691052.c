/**********************************************************************/
/*   ____  ____                                                       */
/*  /   /\/   /                                                       */
/* /___/  \  /                                                        */
/* \   \   \/                                                       */
/*  \   \        Copyright (c) 2003-2009 Xilinx, Inc.                */
/*  /   /          All Right Reserved.                                 */
/* /---/   /\                                                         */
/* \   \  /  \                                                      */
/*  \___\/\___\                                                    */
/***********************************************************************/

/* This file is designed for use with ISim build 0x7708f090 */

#define XSI_HIDE_SYMBOL_SPEC true
#include "xsi.h"
#include <memory.h>
#ifdef __GNUC__
#include <stdlib.h>
#else
#include <malloc.h>
#define alloca _alloca
#endif
static const char *ng0 = "C:/Users/pprice16/Documents/Atlas/Atlas.HDL/CPU/Atlas_CPU/NewSP_TestBench.vhd";
extern char *IEEE_P_1242562249;
extern char *IEEE_P_3620187407;

char *ieee_p_1242562249_sub_180853171_1035706684(char *, char *, int , int );


static void work_a_0156792062_2372691052_p_0(char *t0)
{
    char t3[16];
    char *t1;
    char *t2;
    char *t4;
    char *t5;
    char *t6;
    char *t7;
    char *t8;
    char *t9;
    int64 t10;
    unsigned char t11;

LAB0:    t1 = (t0 + 2832U);
    t2 = *((char **)t1);
    if (t2 == 0)
        goto LAB2;

LAB3:    goto *t2;

LAB2:    xsi_set_current_line(80, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 8, 32);
    t4 = (t0 + 3216);
    t5 = (t4 + 56U);
    t6 = *((char **)t5);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    memcpy(t8, t2, 32U);
    xsi_driver_first_trans_fast(t4);
    xsi_set_current_line(81, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 524296, 32);
    t4 = (t0 + 3280);
    t5 = (t4 + 56U);
    t6 = *((char **)t5);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    memcpy(t8, t2, 32U);
    xsi_driver_first_trans_fast(t4);
    xsi_set_current_line(84, ng0);
    t2 = (t0 + 5860);
    t5 = (t0 + 3344);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 3U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(85, ng0);
    t2 = (t0 + 3408);
    t4 = (t2 + 56U);
    t5 = *((char **)t4);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    *((unsigned char *)t7) = (unsigned char)2;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(87, ng0);
    t10 = (10 * 1000LL);
    t2 = (t0 + 2640);
    xsi_process_wait(t2, t10);

LAB6:    *((char **)t1) = &&LAB7;

LAB1:    return;
LAB4:    xsi_set_current_line(88, ng0);
    t2 = (t0 + 1672U);
    t4 = *((char **)t2);
    t2 = (t0 + 5736U);
    t5 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 8, 32);
    t11 = ieee_std_logic_unsigned_equal_stdv_stdv(IEEE_P_3620187407, t4, t2, t5, t3);
    if (t11 == 0)
        goto LAB8;

LAB9:    xsi_set_current_line(91, ng0);
    t2 = (t0 + 5874);
    t5 = (t0 + 3344);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 3U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(92, ng0);
    t10 = (10 * 1000LL);
    t2 = (t0 + 2640);
    xsi_process_wait(t2, t10);

LAB12:    *((char **)t1) = &&LAB13;
    goto LAB1;

LAB5:    goto LAB4;

LAB7:    goto LAB5;

LAB8:    t6 = (t0 + 5863);
    xsi_report(t6, 11U, 2);
    goto LAB9;

LAB10:    xsi_set_current_line(93, ng0);
    t2 = (t0 + 1672U);
    t4 = *((char **)t2);
    t2 = (t0 + 5736U);
    t5 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 9, 32);
    t11 = ieee_std_logic_unsigned_equal_stdv_stdv(IEEE_P_3620187407, t4, t2, t5, t3);
    if (t11 == 0)
        goto LAB14;

LAB15:    xsi_set_current_line(96, ng0);
    t2 = (t0 + 5888);
    t5 = (t0 + 3344);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 3U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(97, ng0);
    t10 = (10 * 1000LL);
    t2 = (t0 + 2640);
    xsi_process_wait(t2, t10);

LAB18:    *((char **)t1) = &&LAB19;
    goto LAB1;

LAB11:    goto LAB10;

LAB13:    goto LAB11;

LAB14:    t6 = (t0 + 5877);
    xsi_report(t6, 11U, 2);
    goto LAB15;

LAB16:    xsi_set_current_line(98, ng0);
    t2 = (t0 + 1672U);
    t4 = *((char **)t2);
    t2 = (t0 + 5736U);
    t5 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 10, 32);
    t11 = ieee_std_logic_unsigned_equal_stdv_stdv(IEEE_P_3620187407, t4, t2, t5, t3);
    if (t11 == 0)
        goto LAB20;

LAB21:    xsi_set_current_line(101, ng0);
    t2 = (t0 + 5902);
    t5 = (t0 + 3344);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 3U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(102, ng0);
    t10 = (10 * 1000LL);
    t2 = (t0 + 2640);
    xsi_process_wait(t2, t10);

LAB24:    *((char **)t1) = &&LAB25;
    goto LAB1;

LAB17:    goto LAB16;

LAB19:    goto LAB17;

LAB20:    t6 = (t0 + 5891);
    xsi_report(t6, 11U, 2);
    goto LAB21;

LAB22:    xsi_set_current_line(103, ng0);
    t2 = (t0 + 1672U);
    t4 = *((char **)t2);
    t2 = (t0 + 5736U);
    t5 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 7, 32);
    t11 = ieee_std_logic_unsigned_equal_stdv_stdv(IEEE_P_3620187407, t4, t2, t5, t3);
    if (t11 == 0)
        goto LAB26;

LAB27:    xsi_set_current_line(106, ng0);
    t2 = (t0 + 5916);
    t5 = (t0 + 3344);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 3U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(107, ng0);
    t10 = (10 * 1000LL);
    t2 = (t0 + 2640);
    xsi_process_wait(t2, t10);

LAB30:    *((char **)t1) = &&LAB31;
    goto LAB1;

LAB23:    goto LAB22;

LAB25:    goto LAB23;

LAB26:    t6 = (t0 + 5905);
    xsi_report(t6, 11U, 2);
    goto LAB27;

LAB28:    xsi_set_current_line(108, ng0);
    t2 = (t0 + 1672U);
    t4 = *((char **)t2);
    t2 = (t0 + 5736U);
    t5 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 6, 32);
    t11 = ieee_std_logic_unsigned_equal_stdv_stdv(IEEE_P_3620187407, t4, t2, t5, t3);
    if (t11 == 0)
        goto LAB32;

LAB33:    xsi_set_current_line(113, ng0);
    t2 = (t0 + 5930);
    t5 = (t0 + 3344);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 3U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(114, ng0);
    t2 = (t0 + 3408);
    t4 = (t2 + 56U);
    t5 = *((char **)t4);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    *((unsigned char *)t7) = (unsigned char)3;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(115, ng0);
    t10 = (10 * 1000LL);
    t2 = (t0 + 2640);
    xsi_process_wait(t2, t10);

LAB36:    *((char **)t1) = &&LAB37;
    goto LAB1;

LAB29:    goto LAB28;

LAB31:    goto LAB29;

LAB32:    t6 = (t0 + 5919);
    xsi_report(t6, 11U, 2);
    goto LAB33;

LAB34:    xsi_set_current_line(116, ng0);
    t2 = (t0 + 1672U);
    t4 = *((char **)t2);
    t2 = (t0 + 5736U);
    t5 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 524296, 32);
    t11 = ieee_std_logic_unsigned_equal_stdv_stdv(IEEE_P_3620187407, t4, t2, t5, t3);
    if (t11 == 0)
        goto LAB38;

LAB39:    xsi_set_current_line(119, ng0);
    t2 = (t0 + 5944);
    t5 = (t0 + 3344);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 3U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(120, ng0);
    t10 = (10 * 1000LL);
    t2 = (t0 + 2640);
    xsi_process_wait(t2, t10);

LAB42:    *((char **)t1) = &&LAB43;
    goto LAB1;

LAB35:    goto LAB34;

LAB37:    goto LAB35;

LAB38:    t6 = (t0 + 5933);
    xsi_report(t6, 11U, 2);
    goto LAB39;

LAB40:    xsi_set_current_line(121, ng0);
    t2 = (t0 + 1672U);
    t4 = *((char **)t2);
    t2 = (t0 + 5736U);
    t5 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 524297, 32);
    t11 = ieee_std_logic_unsigned_equal_stdv_stdv(IEEE_P_3620187407, t4, t2, t5, t3);
    if (t11 == 0)
        goto LAB44;

LAB45:    xsi_set_current_line(124, ng0);
    t2 = (t0 + 5958);
    t5 = (t0 + 3344);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 3U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(125, ng0);
    t10 = (10 * 1000LL);
    t2 = (t0 + 2640);
    xsi_process_wait(t2, t10);

LAB48:    *((char **)t1) = &&LAB49;
    goto LAB1;

LAB41:    goto LAB40;

LAB43:    goto LAB41;

LAB44:    t6 = (t0 + 5947);
    xsi_report(t6, 11U, 2);
    goto LAB45;

LAB46:    xsi_set_current_line(126, ng0);
    t2 = (t0 + 1672U);
    t4 = *((char **)t2);
    t2 = (t0 + 5736U);
    t5 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 524298, 32);
    t11 = ieee_std_logic_unsigned_equal_stdv_stdv(IEEE_P_3620187407, t4, t2, t5, t3);
    if (t11 == 0)
        goto LAB50;

LAB51:    xsi_set_current_line(129, ng0);
    t2 = (t0 + 5972);
    t5 = (t0 + 3344);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 3U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(130, ng0);
    t10 = (10 * 1000LL);
    t2 = (t0 + 2640);
    xsi_process_wait(t2, t10);

LAB54:    *((char **)t1) = &&LAB55;
    goto LAB1;

LAB47:    goto LAB46;

LAB49:    goto LAB47;

LAB50:    t6 = (t0 + 5961);
    xsi_report(t6, 11U, 2);
    goto LAB51;

LAB52:    xsi_set_current_line(131, ng0);
    t2 = (t0 + 1672U);
    t4 = *((char **)t2);
    t2 = (t0 + 5736U);
    t5 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 524295, 32);
    t11 = ieee_std_logic_unsigned_equal_stdv_stdv(IEEE_P_3620187407, t4, t2, t5, t3);
    if (t11 == 0)
        goto LAB56;

LAB57:    xsi_set_current_line(134, ng0);
    t2 = (t0 + 5986);
    t5 = (t0 + 3344);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 3U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(135, ng0);
    t10 = (10 * 1000LL);
    t2 = (t0 + 2640);
    xsi_process_wait(t2, t10);

LAB60:    *((char **)t1) = &&LAB61;
    goto LAB1;

LAB53:    goto LAB52;

LAB55:    goto LAB53;

LAB56:    t6 = (t0 + 5975);
    xsi_report(t6, 11U, 2);
    goto LAB57;

LAB58:    xsi_set_current_line(136, ng0);
    t2 = (t0 + 1672U);
    t4 = *((char **)t2);
    t2 = (t0 + 5736U);
    t5 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 524294, 32);
    t11 = ieee_std_logic_unsigned_equal_stdv_stdv(IEEE_P_3620187407, t4, t2, t5, t3);
    if (t11 == 0)
        goto LAB62;

LAB63:    xsi_set_current_line(139, ng0);

LAB66:    *((char **)t1) = &&LAB67;
    goto LAB1;

LAB59:    goto LAB58;

LAB61:    goto LAB59;

LAB62:    t6 = (t0 + 5989);
    xsi_report(t6, 11U, 2);
    goto LAB63;

LAB64:    goto LAB2;

LAB65:    goto LAB64;

LAB67:    goto LAB65;

}


extern void work_a_0156792062_2372691052_init()
{
	static char *pe[] = {(void *)work_a_0156792062_2372691052_p_0};
	xsi_register_didat("work_a_0156792062_2372691052", "isim/NewSP_TestBench_isim_beh.exe.sim/work/a_0156792062_2372691052.didat");
	xsi_register_executes(pe);
}
