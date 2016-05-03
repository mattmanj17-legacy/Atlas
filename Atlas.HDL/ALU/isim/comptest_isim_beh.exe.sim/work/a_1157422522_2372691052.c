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
static const char *ng0 = "C:/Users/Matthew/Desktop/ALU/comptest.vhd";



static void work_a_1157422522_2372691052_p_0(char *t0)
{
    char *t1;
    char *t2;
    int64 t3;
    char *t4;
    char *t5;
    char *t6;
    char *t7;
    char *t8;
    char *t9;
    unsigned char t10;
    unsigned char t11;

LAB0:    t1 = (t0 + 3312U);
    t2 = *((char **)t1);
    if (t2 == 0)
        goto LAB2;

LAB3:    goto *t2;

LAB2:    xsi_set_current_line(86, ng0);
    t3 = (100 * 1000LL);
    t2 = (t0 + 3120);
    xsi_process_wait(t2, t3);

LAB6:    *((char **)t1) = &&LAB7;

LAB1:    return;
LAB4:    xsi_set_current_line(88, ng0);
    t2 = (t0 + 5762);
    t5 = (t0 + 3696);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 8U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(89, ng0);
    t2 = (t0 + 5770);
    t5 = (t0 + 3760);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 8U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(90, ng0);
    t3 = (10 * 1000LL);
    t2 = (t0 + 3120);
    xsi_process_wait(t2, t3);

LAB10:    *((char **)t1) = &&LAB11;
    goto LAB1;

LAB5:    goto LAB4;

LAB7:    goto LAB5;

LAB8:    xsi_set_current_line(92, ng0);
    t2 = (t0 + 1352U);
    t4 = *((char **)t2);
    t10 = *((unsigned char *)t4);
    t11 = (t10 == (unsigned char)3);
    if (t11 == 0)
        goto LAB12;

LAB13:    xsi_set_current_line(93, ng0);
    t2 = (t0 + 1512U);
    t4 = *((char **)t2);
    t10 = *((unsigned char *)t4);
    t11 = (t10 == (unsigned char)2);
    if (t11 == 0)
        goto LAB14;

LAB15:    xsi_set_current_line(94, ng0);
    t2 = (t0 + 1672U);
    t4 = *((char **)t2);
    t10 = *((unsigned char *)t4);
    t11 = (t10 == (unsigned char)2);
    if (t11 == 0)
        goto LAB16;

LAB17:    xsi_set_current_line(95, ng0);
    t2 = (t0 + 1832U);
    t4 = *((char **)t2);
    t10 = *((unsigned char *)t4);
    t11 = (t10 == (unsigned char)3);
    if (t11 == 0)
        goto LAB18;

LAB19:    xsi_set_current_line(96, ng0);
    t2 = (t0 + 1992U);
    t4 = *((char **)t2);
    t10 = *((unsigned char *)t4);
    t11 = (t10 == (unsigned char)2);
    if (t11 == 0)
        goto LAB20;

LAB21:    xsi_set_current_line(97, ng0);
    t2 = (t0 + 2152U);
    t4 = *((char **)t2);
    t10 = *((unsigned char *)t4);
    t11 = (t10 == (unsigned char)3);
    if (t11 == 0)
        goto LAB22;

LAB23:    xsi_set_current_line(99, ng0);
    t3 = (10 * 1000LL);
    t2 = (t0 + 3120);
    xsi_process_wait(t2, t3);

LAB26:    *((char **)t1) = &&LAB27;
    goto LAB1;

LAB9:    goto LAB8;

LAB11:    goto LAB9;

LAB12:    t2 = (t0 + 5778);
    xsi_report(t2, 11U, 2);
    goto LAB13;

LAB14:    t2 = (t0 + 5789);
    xsi_report(t2, 12U, 2);
    goto LAB15;

LAB16:    t2 = (t0 + 5801);
    xsi_report(t2, 11U, 2);
    goto LAB17;

LAB18:    t2 = (t0 + 5812);
    xsi_report(t2, 12U, 2);
    goto LAB19;

LAB20:    t2 = (t0 + 5824);
    xsi_report(t2, 11U, 2);
    goto LAB21;

LAB22:    t2 = (t0 + 5835);
    xsi_report(t2, 12U, 2);
    goto LAB23;

LAB24:    xsi_set_current_line(101, ng0);
    t2 = (t0 + 5847);
    t5 = (t0 + 3696);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 8U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(102, ng0);
    t2 = (t0 + 5855);
    t5 = (t0 + 3760);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 8U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(103, ng0);
    t3 = (10 * 1000LL);
    t2 = (t0 + 3120);
    xsi_process_wait(t2, t3);

LAB30:    *((char **)t1) = &&LAB31;
    goto LAB1;

LAB25:    goto LAB24;

LAB27:    goto LAB25;

LAB28:    xsi_set_current_line(105, ng0);
    t2 = (t0 + 1352U);
    t4 = *((char **)t2);
    t10 = *((unsigned char *)t4);
    t11 = (t10 == (unsigned char)2);
    if (t11 == 0)
        goto LAB32;

LAB33:    xsi_set_current_line(106, ng0);
    t2 = (t0 + 1512U);
    t4 = *((char **)t2);
    t10 = *((unsigned char *)t4);
    t11 = (t10 == (unsigned char)3);
    if (t11 == 0)
        goto LAB34;

LAB35:    xsi_set_current_line(107, ng0);
    t2 = (t0 + 1672U);
    t4 = *((char **)t2);
    t10 = *((unsigned char *)t4);
    t11 = (t10 == (unsigned char)3);
    if (t11 == 0)
        goto LAB36;

LAB37:    xsi_set_current_line(108, ng0);
    t2 = (t0 + 1832U);
    t4 = *((char **)t2);
    t10 = *((unsigned char *)t4);
    t11 = (t10 == (unsigned char)3);
    if (t11 == 0)
        goto LAB38;

LAB39:    xsi_set_current_line(109, ng0);
    t2 = (t0 + 1992U);
    t4 = *((char **)t2);
    t10 = *((unsigned char *)t4);
    t11 = (t10 == (unsigned char)2);
    if (t11 == 0)
        goto LAB40;

LAB41:    xsi_set_current_line(110, ng0);
    t2 = (t0 + 2152U);
    t4 = *((char **)t2);
    t10 = *((unsigned char *)t4);
    t11 = (t10 == (unsigned char)2);
    if (t11 == 0)
        goto LAB42;

LAB43:    xsi_set_current_line(112, ng0);

LAB46:    *((char **)t1) = &&LAB47;
    goto LAB1;

LAB29:    goto LAB28;

LAB31:    goto LAB29;

LAB32:    t2 = (t0 + 5863);
    xsi_report(t2, 11U, 2);
    goto LAB33;

LAB34:    t2 = (t0 + 5874);
    xsi_report(t2, 12U, 2);
    goto LAB35;

LAB36:    t2 = (t0 + 5886);
    xsi_report(t2, 11U, 2);
    goto LAB37;

LAB38:    t2 = (t0 + 5897);
    xsi_report(t2, 12U, 2);
    goto LAB39;

LAB40:    t2 = (t0 + 5909);
    xsi_report(t2, 11U, 2);
    goto LAB41;

LAB42:    t2 = (t0 + 5920);
    xsi_report(t2, 12U, 2);
    goto LAB43;

LAB44:    goto LAB2;

LAB45:    goto LAB44;

LAB47:    goto LAB45;

}


extern void work_a_1157422522_2372691052_init()
{
	static char *pe[] = {(void *)work_a_1157422522_2372691052_p_0};
	xsi_register_didat("work_a_1157422522_2372691052", "isim/comptest_isim_beh.exe.sim/work/a_1157422522_2372691052.didat");
	xsi_register_executes(pe);
}
