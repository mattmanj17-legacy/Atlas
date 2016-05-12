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
static const char *ng0 = "C:/Users/pprice16/Documents/Atlas/Atlas.HDL/CPU/Atlas_CPU/ShiftTestBench.vhd";
extern char *IEEE_P_1242562249;

char *ieee_p_1242562249_sub_180853171_1035706684(char *, char *, int , int );
char *ieee_p_1242562249_sub_2540846514_1035706684(char *, char *, char *, char *, int );
char *ieee_p_1242562249_sub_2547962040_1035706684(char *, char *, char *, char *, int );


static void work_a_0600852594_2372691052_p_0(char *t0)
{
    char t11[16];
    char *t1;
    char *t2;
    char *t3;
    char *t4;
    char *t5;
    char *t6;
    char *t7;
    char *t8;
    int t9;
    int t10;
    char *t12;
    char *t13;
    int64 t14;
    unsigned int t15;
    unsigned int t16;
    unsigned char t17;
    unsigned int t18;
    int t20;

LAB0:    t1 = (t0 + 2672U);
    t2 = *((char **)t1);
    if (t2 == 0)
        goto LAB2;

LAB3:    goto *t2;

LAB2:    xsi_set_current_line(76, ng0);
    t2 = (t0 + 5390);
    t4 = (t0 + 3056);
    t5 = (t4 + 56U);
    t6 = *((char **)t5);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    memcpy(t8, t2, 32U);
    xsi_driver_first_trans_fast(t4);
    xsi_set_current_line(78, ng0);
    t2 = (t0 + 3120);
    t3 = (t2 + 56U);
    t4 = *((char **)t3);
    t5 = (t4 + 56U);
    t6 = *((char **)t5);
    *((unsigned char *)t6) = (unsigned char)2;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(80, ng0);
    t2 = (t0 + 5422);
    *((int *)t2) = 0;
    t3 = (t0 + 5426);
    *((int *)t3) = 32;
    t9 = 0;
    t10 = 32;

LAB4:    if (t9 <= t10)
        goto LAB5;

LAB7:    xsi_set_current_line(86, ng0);
    t2 = (t0 + 3120);
    t3 = (t2 + 56U);
    t4 = *((char **)t3);
    t5 = (t4 + 56U);
    t6 = *((char **)t5);
    *((unsigned char *)t6) = (unsigned char)3;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(88, ng0);
    t2 = (t0 + 5447);
    *((int *)t2) = 0;
    t3 = (t0 + 5451);
    *((int *)t3) = 32;
    t9 = 0;
    t10 = 32;

LAB21:    if (t9 <= t10)
        goto LAB22;

LAB24:    xsi_set_current_line(94, ng0);

LAB40:    *((char **)t1) = &&LAB41;

LAB1:    return;
LAB5:    xsi_set_current_line(81, ng0);
    t4 = (t0 + 5422);
    t5 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t11, *((int *)t4), 5);
    t6 = (t0 + 3184);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    t12 = (t8 + 56U);
    t13 = *((char **)t12);
    memcpy(t13, t5, 5U);
    xsi_driver_first_trans_fast(t6);
    xsi_set_current_line(82, ng0);
    t14 = (5 * 1000LL);
    t2 = (t0 + 2480);
    xsi_process_wait(t2, t14);

LAB10:    *((char **)t1) = &&LAB11;
    goto LAB1;

LAB6:    t2 = (t0 + 5422);
    t9 = *((int *)t2);
    t3 = (t0 + 5426);
    t10 = *((int *)t3);
    if (t9 == t10)
        goto LAB7;

LAB20:    t20 = (t9 + 1);
    t9 = t20;
    t4 = (t0 + 5422);
    *((int *)t4) = t9;
    goto LAB4;

LAB8:    xsi_set_current_line(83, ng0);
    t2 = (t0 + 1512U);
    t3 = *((char **)t2);
    t2 = (t0 + 1032U);
    t4 = *((char **)t2);
    t2 = (t0 + 5264U);
    t5 = (t0 + 5422);
    t6 = ieee_p_1242562249_sub_2540846514_1035706684(IEEE_P_1242562249, t11, t4, t2, *((int *)t5));
    t7 = (t11 + 12U);
    t15 = *((unsigned int *)t7);
    t16 = (1U * t15);
    t17 = 1;
    if (32U == t16)
        goto LAB14;

LAB15:    t17 = 0;

LAB16:    if (t17 == 0)
        goto LAB12;

LAB13:    goto LAB6;

LAB9:    goto LAB8;

LAB11:    goto LAB9;

LAB12:    t13 = (t0 + 5430);
    xsi_report(t13, 17U, 2);
    goto LAB13;

LAB14:    t18 = 0;

LAB17:    if (t18 < 32U)
        goto LAB18;
    else
        goto LAB16;

LAB18:    t8 = (t3 + t18);
    t12 = (t6 + t18);
    if (*((unsigned char *)t8) != *((unsigned char *)t12))
        goto LAB15;

LAB19:    t18 = (t18 + 1);
    goto LAB17;

LAB22:    xsi_set_current_line(89, ng0);
    t4 = (t0 + 5447);
    t5 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t11, *((int *)t4), 5);
    t6 = (t0 + 3184);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    t12 = (t8 + 56U);
    t13 = *((char **)t12);
    memcpy(t13, t5, 5U);
    xsi_driver_first_trans_fast(t6);
    xsi_set_current_line(90, ng0);
    t14 = (5 * 1000LL);
    t2 = (t0 + 2480);
    xsi_process_wait(t2, t14);

LAB27:    *((char **)t1) = &&LAB28;
    goto LAB1;

LAB23:    t2 = (t0 + 5447);
    t9 = *((int *)t2);
    t3 = (t0 + 5451);
    t10 = *((int *)t3);
    if (t9 == t10)
        goto LAB24;

LAB37:    t20 = (t9 + 1);
    t9 = t20;
    t4 = (t0 + 5447);
    *((int *)t4) = t9;
    goto LAB21;

LAB25:    xsi_set_current_line(91, ng0);
    t2 = (t0 + 1512U);
    t3 = *((char **)t2);
    t2 = (t0 + 1032U);
    t4 = *((char **)t2);
    t2 = (t0 + 5264U);
    t5 = (t0 + 5447);
    t6 = ieee_p_1242562249_sub_2547962040_1035706684(IEEE_P_1242562249, t11, t4, t2, *((int *)t5));
    t7 = (t11 + 12U);
    t15 = *((unsigned int *)t7);
    t16 = (1U * t15);
    t17 = 1;
    if (32U == t16)
        goto LAB31;

LAB32:    t17 = 0;

LAB33:    if (t17 == 0)
        goto LAB29;

LAB30:    goto LAB23;

LAB26:    goto LAB25;

LAB28:    goto LAB26;

LAB29:    t13 = (t0 + 5455);
    xsi_report(t13, 18U, 2);
    goto LAB30;

LAB31:    t18 = 0;

LAB34:    if (t18 < 32U)
        goto LAB35;
    else
        goto LAB33;

LAB35:    t8 = (t3 + t18);
    t12 = (t6 + t18);
    if (*((unsigned char *)t8) != *((unsigned char *)t12))
        goto LAB32;

LAB36:    t18 = (t18 + 1);
    goto LAB34;

LAB38:    goto LAB2;

LAB39:    goto LAB38;

LAB41:    goto LAB39;

}


extern void work_a_0600852594_2372691052_init()
{
	static char *pe[] = {(void *)work_a_0600852594_2372691052_p_0};
	xsi_register_didat("work_a_0600852594_2372691052", "isim/ShiftTestBench_isim_beh.exe.sim/work/a_0600852594_2372691052.didat");
	xsi_register_executes(pe);
}
