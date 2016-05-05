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
static const char *ng0 = "C:/Users/pprice16/Documents/Atlas/Atlas.HDL/CPU/Atlas_CPU/NewPC_TestBench.vhd";
extern char *IEEE_P_1242562249;
extern char *IEEE_P_3620187407;

char *ieee_p_1242562249_sub_180853171_1035706684(char *, char *, int , int );
char *ieee_p_3620187407_sub_767668596_3965413181(char *, char *, char *, char *, char *, char *);


static void work_a_1238500548_2372691052_p_0(char *t0)
{
    char t3[16];
    char t12[16];
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

LAB0:    t1 = (t0 + 3152U);
    t2 = *((char **)t1);
    if (t2 == 0)
        goto LAB2;

LAB3:    goto *t2;

LAB2:    xsi_set_current_line(86, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 0, 32);
    t4 = (t0 + 3536);
    t5 = (t4 + 56U);
    t6 = *((char **)t5);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    memcpy(t8, t2, 32U);
    xsi_driver_first_trans_fast(t4);
    xsi_set_current_line(88, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 268435455, 32);
    t4 = (t0 + 3600);
    t5 = (t4 + 56U);
    t6 = *((char **)t5);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    memcpy(t8, t2, 32U);
    xsi_driver_first_trans_fast(t4);
    xsi_set_current_line(89, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 0, 32);
    t4 = (t0 + 3664);
    t5 = (t4 + 56U);
    t6 = *((char **)t5);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    memcpy(t8, t2, 32U);
    xsi_driver_first_trans_fast(t4);
    xsi_set_current_line(90, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 268435454, 32);
    t4 = (t0 + 3728);
    t5 = (t4 + 56U);
    t6 = *((char **)t5);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    memcpy(t8, t2, 32U);
    xsi_driver_first_trans_fast(t4);
    xsi_set_current_line(91, ng0);
    t2 = (t0 + 3792);
    t4 = (t2 + 56U);
    t5 = *((char **)t4);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    *((unsigned char *)t7) = (unsigned char)2;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(92, ng0);
    t2 = (t0 + 6555);
    t5 = (t0 + 3856);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 2U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(95, ng0);
    t10 = (20 * 1000LL);
    t2 = (t0 + 2960);
    xsi_process_wait(t2, t10);

LAB6:    *((char **)t1) = &&LAB7;

LAB1:    return;
LAB4:    xsi_set_current_line(96, ng0);
    t2 = (t0 + 1992U);
    t4 = *((char **)t2);
    t2 = (t0 + 6368U);
    t5 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 1, 32);
    t11 = ieee_std_logic_unsigned_equal_stdv_stdv(IEEE_P_3620187407, t4, t2, t5, t3);
    if (t11 == 0)
        goto LAB8;

LAB9:    xsi_set_current_line(99, ng0);
    t2 = (t0 + 3792);
    t4 = (t2 + 56U);
    t5 = *((char **)t4);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    *((unsigned char *)t7) = (unsigned char)3;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(100, ng0);
    t10 = (20 * 1000LL);
    t2 = (t0 + 2960);
    xsi_process_wait(t2, t10);

LAB12:    *((char **)t1) = &&LAB13;
    goto LAB1;

LAB5:    goto LAB4;

LAB7:    goto LAB5;

LAB8:    t6 = (t0 + 6557);
    xsi_report(t6, 19U, 2);
    goto LAB9;

LAB10:    xsi_set_current_line(101, ng0);
    t2 = (t0 + 1992U);
    t4 = *((char **)t2);
    t2 = (t0 + 6368U);
    t5 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 2, 32);
    t11 = ieee_std_logic_unsigned_equal_stdv_stdv(IEEE_P_3620187407, t4, t2, t5, t3);
    if (t11 == 0)
        goto LAB14;

LAB15:    xsi_set_current_line(105, ng0);
    t2 = (t0 + 6595);
    t5 = (t0 + 3856);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 2U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(106, ng0);
    t10 = (20 * 1000LL);
    t2 = (t0 + 2960);
    xsi_process_wait(t2, t10);

LAB18:    *((char **)t1) = &&LAB19;
    goto LAB1;

LAB11:    goto LAB10;

LAB13:    goto LAB11;

LAB14:    t6 = (t0 + 6576);
    xsi_report(t6, 19U, 2);
    goto LAB15;

LAB16:    xsi_set_current_line(107, ng0);
    t2 = (t0 + 1992U);
    t4 = *((char **)t2);
    t2 = (t0 + 6368U);
    t5 = (t0 + 1512U);
    t6 = *((char **)t5);
    t5 = (t0 + 6336U);
    t11 = ieee_std_logic_unsigned_equal_stdv_stdv(IEEE_P_3620187407, t4, t2, t6, t5);
    if (t11 == 0)
        goto LAB20;

LAB21:    xsi_set_current_line(111, ng0);
    t2 = (t0 + 6621);
    t5 = (t0 + 3856);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 2U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(112, ng0);
    t2 = (t0 + 3792);
    t4 = (t2 + 56U);
    t5 = *((char **)t4);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    *((unsigned char *)t7) = (unsigned char)2;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(113, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 1, 32);
    t4 = (t0 + 3664);
    t5 = (t4 + 56U);
    t6 = *((char **)t5);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    memcpy(t8, t2, 32U);
    xsi_driver_first_trans_fast(t4);
    xsi_set_current_line(114, ng0);
    t10 = (20 * 1000LL);
    t2 = (t0 + 2960);
    xsi_process_wait(t2, t10);

LAB24:    *((char **)t1) = &&LAB25;
    goto LAB1;

LAB17:    goto LAB16;

LAB19:    goto LAB17;

LAB20:    t7 = (t0 + 6597);
    xsi_report(t7, 24U, 2);
    goto LAB21;

LAB22:    xsi_set_current_line(115, ng0);
    t2 = (t0 + 1992U);
    t4 = *((char **)t2);
    t2 = (t0 + 6368U);
    t5 = (t0 + 1512U);
    t6 = *((char **)t5);
    t5 = (t0 + 6336U);
    t11 = ieee_std_logic_unsigned_equal_stdv_stdv(IEEE_P_3620187407, t4, t2, t6, t5);
    if (t11 == 0)
        goto LAB26;

LAB27:    xsi_set_current_line(118, ng0);
    t2 = (t0 + 6660);
    t5 = (t0 + 3856);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 2U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(119, ng0);
    t2 = (t0 + 3792);
    t4 = (t2 + 56U);
    t5 = *((char **)t4);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    *((unsigned char *)t7) = (unsigned char)2;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(120, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 0, 32);
    t4 = (t0 + 3664);
    t5 = (t4 + 56U);
    t6 = *((char **)t5);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    memcpy(t8, t2, 32U);
    xsi_driver_first_trans_fast(t4);
    xsi_set_current_line(121, ng0);
    t10 = (20 * 1000LL);
    t2 = (t0 + 2960);
    xsi_process_wait(t2, t10);

LAB30:    *((char **)t1) = &&LAB31;
    goto LAB1;

LAB23:    goto LAB22;

LAB25:    goto LAB23;

LAB26:    t7 = (t0 + 6623);
    xsi_report(t7, 37U, 2);
    goto LAB27;

LAB28:    xsi_set_current_line(122, ng0);
    t2 = (t0 + 1992U);
    t4 = *((char **)t2);
    t2 = (t0 + 6368U);
    t5 = (t0 + 1032U);
    t6 = *((char **)t5);
    t5 = (t0 + 6288U);
    t7 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t12, 1, 32);
    t8 = ieee_p_3620187407_sub_767668596_3965413181(IEEE_P_3620187407, t3, t6, t5, t7, t12);
    t11 = ieee_std_logic_unsigned_equal_stdv_stdv(IEEE_P_3620187407, t4, t2, t8, t3);
    if (t11 == 0)
        goto LAB32;

LAB33:    xsi_set_current_line(127, ng0);
    t2 = (t0 + 6700);
    t5 = (t0 + 3856);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 2U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(128, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 252579084, 32);
    t4 = (t0 + 3728);
    t5 = (t4 + 56U);
    t6 = *((char **)t5);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    memcpy(t8, t2, 32U);
    xsi_driver_first_trans_fast(t4);
    xsi_set_current_line(129, ng0);
    t10 = (20 * 1000LL);
    t2 = (t0 + 2960);
    xsi_process_wait(t2, t10);

LAB36:    *((char **)t1) = &&LAB37;
    goto LAB1;

LAB29:    goto LAB28;

LAB31:    goto LAB29;

LAB32:    t9 = (t0 + 6662);
    xsi_report(t9, 38U, 2);
    goto LAB33;

LAB34:    xsi_set_current_line(130, ng0);
    t2 = (t0 + 1992U);
    t4 = *((char **)t2);
    t2 = (t0 + 6368U);
    t5 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t3, 252579084, 32);
    t11 = ieee_std_logic_unsigned_equal_stdv_stdv(IEEE_P_3620187407, t4, t2, t5, t3);
    if (t11 == 0)
        goto LAB38;

LAB39:    xsi_set_current_line(133, ng0);

LAB42:    *((char **)t1) = &&LAB43;
    goto LAB1;

LAB35:    goto LAB34;

LAB37:    goto LAB35;

LAB38:    t6 = (t0 + 6702);
    xsi_report(t6, 13U, 2);
    goto LAB39;

LAB40:    goto LAB2;

LAB41:    goto LAB40;

LAB43:    goto LAB41;

}


extern void work_a_1238500548_2372691052_init()
{
	static char *pe[] = {(void *)work_a_1238500548_2372691052_p_0};
	xsi_register_didat("work_a_1238500548_2372691052", "isim/NewPC_TestBench_isim_beh.exe.sim/work/a_1238500548_2372691052.didat");
	xsi_register_executes(pe);
}
