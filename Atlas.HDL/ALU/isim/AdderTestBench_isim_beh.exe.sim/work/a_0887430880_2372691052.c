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
static const char *ng0 = "C:/Users/Matthew/Desktop/ALU/AdderTestBench.vhd";
extern char *IEEE_P_1242562249;

char *ieee_p_1242562249_sub_180853171_1035706684(char *, char *, int , int );


static void work_a_0887430880_2372691052_p_0(char *t0)
{
    char t4[16];
    char *t1;
    char *t2;
    int64 t3;
    char *t5;
    char *t6;
    char *t7;
    char *t8;
    char *t9;
    unsigned int t10;
    unsigned char t11;
    unsigned int t12;

LAB0:    t1 = (t0 + 2832U);
    t2 = *((char **)t1);
    if (t2 == 0)
        goto LAB2;

LAB3:    goto *t2;

LAB2:    xsi_set_current_line(81, ng0);
    t3 = (100 * 1000LL);
    t2 = (t0 + 2640);
    xsi_process_wait(t2, t3);

LAB6:    *((char **)t1) = &&LAB7;

LAB1:    return;
LAB4:    xsi_set_current_line(82, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t4, 7, 8);
    t5 = (t0 + 3216);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 8U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(83, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t4, 7, 8);
    t5 = (t0 + 3280);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 8U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(84, ng0);
    t2 = (t0 + 3344);
    t5 = (t2 + 56U);
    t6 = *((char **)t5);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    *((unsigned char *)t8) = (unsigned char)2;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(85, ng0);
    t3 = (10 * 1000LL);
    t2 = (t0 + 2640);
    xsi_process_wait(t2, t3);

LAB10:    *((char **)t1) = &&LAB11;
    goto LAB1;

LAB5:    goto LAB4;

LAB7:    goto LAB5;

LAB8:    xsi_set_current_line(86, ng0);
    t2 = (t0 + 1512U);
    t5 = *((char **)t2);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t4, 14, 8);
    t6 = (t4 + 12U);
    t10 = *((unsigned int *)t6);
    t10 = (t10 * 1U);
    t11 = 1;
    if (8U == t10)
        goto LAB14;

LAB15:    t11 = 0;

LAB16:    if (t11 == 0)
        goto LAB12;

LAB13:    xsi_set_current_line(88, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t4, 8, 8);
    t5 = (t0 + 3216);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 8U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(89, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t4, 1, 8);
    t5 = (t0 + 3280);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 8U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(90, ng0);
    t2 = (t0 + 3344);
    t5 = (t2 + 56U);
    t6 = *((char **)t5);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    *((unsigned char *)t8) = (unsigned char)3;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(91, ng0);
    t3 = (10 * 1000LL);
    t2 = (t0 + 2640);
    xsi_process_wait(t2, t3);

LAB22:    *((char **)t1) = &&LAB23;
    goto LAB1;

LAB9:    goto LAB8;

LAB11:    goto LAB9;

LAB12:    t9 = (t0 + 5382);
    xsi_report(t9, 13U, 2);
    goto LAB13;

LAB14:    t12 = 0;

LAB17:    if (t12 < 8U)
        goto LAB18;
    else
        goto LAB16;

LAB18:    t7 = (t5 + t12);
    t8 = (t2 + t12);
    if (*((unsigned char *)t7) != *((unsigned char *)t8))
        goto LAB15;

LAB19:    t12 = (t12 + 1);
    goto LAB17;

LAB20:    xsi_set_current_line(92, ng0);
    t2 = (t0 + 1512U);
    t5 = *((char **)t2);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t4, 10, 8);
    t6 = (t4 + 12U);
    t10 = *((unsigned int *)t6);
    t10 = (t10 * 1U);
    t11 = 1;
    if (8U == t10)
        goto LAB26;

LAB27:    t11 = 0;

LAB28:    if (t11 == 0)
        goto LAB24;

LAB25:    xsi_set_current_line(94, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t4, 64, 8);
    t5 = (t0 + 3216);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 8U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(95, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t4, 84, 8);
    t5 = (t0 + 3280);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 8U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(96, ng0);
    t2 = (t0 + 3344);
    t5 = (t2 + 56U);
    t6 = *((char **)t5);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    *((unsigned char *)t8) = (unsigned char)2;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(97, ng0);
    t3 = (10 * 1000LL);
    t2 = (t0 + 2640);
    xsi_process_wait(t2, t3);

LAB34:    *((char **)t1) = &&LAB35;
    goto LAB1;

LAB21:    goto LAB20;

LAB23:    goto LAB21;

LAB24:    t9 = (t0 + 5395);
    xsi_report(t9, 21U, 2);
    goto LAB25;

LAB26:    t12 = 0;

LAB29:    if (t12 < 8U)
        goto LAB30;
    else
        goto LAB28;

LAB30:    t7 = (t5 + t12);
    t8 = (t2 + t12);
    if (*((unsigned char *)t7) != *((unsigned char *)t8))
        goto LAB27;

LAB31:    t12 = (t12 + 1);
    goto LAB29;

LAB32:    xsi_set_current_line(98, ng0);
    t2 = (t0 + 1512U);
    t5 = *((char **)t2);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t4, 148, 8);
    t6 = (t4 + 12U);
    t10 = *((unsigned int *)t6);
    t10 = (t10 * 1U);
    t11 = 1;
    if (8U == t10)
        goto LAB38;

LAB39:    t11 = 0;

LAB40:    if (t11 == 0)
        goto LAB36;

LAB37:    xsi_set_current_line(100, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t4, 46, 8);
    t5 = (t0 + 3216);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 8U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(101, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t4, 12, 8);
    t5 = (t0 + 3280);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 8U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(102, ng0);
    t2 = (t0 + 3344);
    t5 = (t2 + 56U);
    t6 = *((char **)t5);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    *((unsigned char *)t8) = (unsigned char)2;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(103, ng0);
    t3 = (10 * 1000LL);
    t2 = (t0 + 2640);
    xsi_process_wait(t2, t3);

LAB46:    *((char **)t1) = &&LAB47;
    goto LAB1;

LAB33:    goto LAB32;

LAB35:    goto LAB33;

LAB36:    t9 = (t0 + 5416);
    xsi_report(t9, 15U, 2);
    goto LAB37;

LAB38:    t12 = 0;

LAB41:    if (t12 < 8U)
        goto LAB42;
    else
        goto LAB40;

LAB42:    t7 = (t5 + t12);
    t8 = (t2 + t12);
    if (*((unsigned char *)t7) != *((unsigned char *)t8))
        goto LAB39;

LAB43:    t12 = (t12 + 1);
    goto LAB41;

LAB44:    xsi_set_current_line(104, ng0);
    t2 = (t0 + 1512U);
    t5 = *((char **)t2);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t4, 58, 8);
    t6 = (t4 + 12U);
    t10 = *((unsigned int *)t6);
    t10 = (t10 * 1U);
    t11 = 1;
    if (8U == t10)
        goto LAB50;

LAB51:    t11 = 0;

LAB52:    if (t11 == 0)
        goto LAB48;

LAB49:    xsi_set_current_line(106, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t4, 1, 8);
    t5 = (t0 + 3216);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 8U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(107, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t4, 0, 8);
    t5 = (t0 + 3280);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 8U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(108, ng0);
    t2 = (t0 + 3344);
    t5 = (t2 + 56U);
    t6 = *((char **)t5);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    *((unsigned char *)t8) = (unsigned char)2;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(109, ng0);
    t3 = (10 * 1000LL);
    t2 = (t0 + 2640);
    xsi_process_wait(t2, t3);

LAB58:    *((char **)t1) = &&LAB59;
    goto LAB1;

LAB45:    goto LAB44;

LAB47:    goto LAB45;

LAB48:    t9 = (t0 + 5431);
    xsi_report(t9, 15U, 2);
    goto LAB49;

LAB50:    t12 = 0;

LAB53:    if (t12 < 8U)
        goto LAB54;
    else
        goto LAB52;

LAB54:    t7 = (t5 + t12);
    t8 = (t2 + t12);
    if (*((unsigned char *)t7) != *((unsigned char *)t8))
        goto LAB51;

LAB55:    t12 = (t12 + 1);
    goto LAB53;

LAB56:    xsi_set_current_line(110, ng0);
    t2 = (t0 + 1512U);
    t5 = *((char **)t2);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t4, 1, 8);
    t6 = (t4 + 12U);
    t10 = *((unsigned int *)t6);
    t10 = (t10 * 1U);
    t11 = 1;
    if (8U == t10)
        goto LAB62;

LAB63:    t11 = 0;

LAB64:    if (t11 == 0)
        goto LAB60;

LAB61:    xsi_set_current_line(112, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t4, 2, 8);
    t5 = (t0 + 3216);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 8U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(113, ng0);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t4, 15, 8);
    t5 = (t0 + 3280);
    t6 = (t5 + 56U);
    t7 = *((char **)t6);
    t8 = (t7 + 56U);
    t9 = *((char **)t8);
    memcpy(t9, t2, 8U);
    xsi_driver_first_trans_fast(t5);
    xsi_set_current_line(114, ng0);
    t2 = (t0 + 3344);
    t5 = (t2 + 56U);
    t6 = *((char **)t5);
    t7 = (t6 + 56U);
    t8 = *((char **)t7);
    *((unsigned char *)t8) = (unsigned char)3;
    xsi_driver_first_trans_fast(t2);
    xsi_set_current_line(115, ng0);
    t3 = (10 * 1000LL);
    t2 = (t0 + 2640);
    xsi_process_wait(t2, t3);

LAB70:    *((char **)t1) = &&LAB71;
    goto LAB1;

LAB57:    goto LAB56;

LAB59:    goto LAB57;

LAB60:    t9 = (t0 + 5446);
    xsi_report(t9, 13U, 2);
    goto LAB61;

LAB62:    t12 = 0;

LAB65:    if (t12 < 8U)
        goto LAB66;
    else
        goto LAB64;

LAB66:    t7 = (t5 + t12);
    t8 = (t2 + t12);
    if (*((unsigned char *)t7) != *((unsigned char *)t8))
        goto LAB63;

LAB67:    t12 = (t12 + 1);
    goto LAB65;

LAB68:    xsi_set_current_line(116, ng0);
    t2 = (t0 + 1512U);
    t5 = *((char **)t2);
    t2 = ieee_p_1242562249_sub_180853171_1035706684(IEEE_P_1242562249, t4, 18, 8);
    t6 = (t4 + 12U);
    t10 = *((unsigned int *)t6);
    t10 = (t10 * 1U);
    t11 = 1;
    if (8U == t10)
        goto LAB74;

LAB75:    t11 = 0;

LAB76:    if (t11 == 0)
        goto LAB72;

LAB73:    xsi_set_current_line(121, ng0);

LAB82:    *((char **)t1) = &&LAB83;
    goto LAB1;

LAB69:    goto LAB68;

LAB71:    goto LAB69;

LAB72:    t9 = (t0 + 5459);
    xsi_report(t9, 22U, 2);
    goto LAB73;

LAB74:    t12 = 0;

LAB77:    if (t12 < 8U)
        goto LAB78;
    else
        goto LAB76;

LAB78:    t7 = (t5 + t12);
    t8 = (t2 + t12);
    if (*((unsigned char *)t7) != *((unsigned char *)t8))
        goto LAB75;

LAB79:    t12 = (t12 + 1);
    goto LAB77;

LAB80:    goto LAB2;

LAB81:    goto LAB80;

LAB83:    goto LAB81;

}


extern void work_a_0887430880_2372691052_init()
{
	static char *pe[] = {(void *)work_a_0887430880_2372691052_p_0};
	xsi_register_didat("work_a_0887430880_2372691052", "isim/AdderTestBench_isim_beh.exe.sim/work/a_0887430880_2372691052.didat");
	xsi_register_executes(pe);
}
