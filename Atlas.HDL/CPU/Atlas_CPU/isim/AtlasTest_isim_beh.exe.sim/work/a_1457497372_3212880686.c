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
static const char *ng0 = "C:/Users/mdraper16/Documents/Atlas/Atlas.HDL/CPU/Atlas_CPU/SelectWData.vhd";



static void work_a_1457497372_3212880686_p_0(char *t0)
{
    char *t1;
    char *t2;
    unsigned char t4;
    unsigned int t5;
    char *t6;
    char *t7;
    char *t8;
    char *t9;
    char *t10;
    char *t11;
    char *t12;
    char *t13;
    char *t14;
    char *t15;
    unsigned char t17;
    unsigned int t18;
    char *t19;
    char *t20;
    char *t21;
    char *t22;
    char *t23;
    char *t24;
    char *t25;
    char *t26;
    char *t27;
    char *t28;
    unsigned char t30;
    unsigned int t31;
    char *t32;
    char *t33;
    char *t34;
    char *t35;
    char *t36;
    char *t37;
    char *t38;
    char *t39;
    char *t40;
    char *t41;
    unsigned char t43;
    unsigned int t44;
    char *t45;
    char *t46;
    char *t47;
    char *t48;
    char *t49;
    char *t50;
    char *t51;
    char *t52;
    char *t53;
    char *t54;
    unsigned char t56;
    unsigned int t57;
    char *t58;
    char *t59;
    char *t60;
    char *t61;
    char *t62;
    char *t63;
    char *t64;
    char *t65;
    char *t66;
    char *t67;
    char *t68;
    char *t69;
    char *t70;
    char *t71;
    char *t72;

LAB0:    xsi_set_current_line(49, ng0);
    t1 = (t0 + 1992U);
    t2 = *((char **)t1);
    t1 = (t0 + 6183);
    t4 = 1;
    if (3U == 3U)
        goto LAB5;

LAB6:    t4 = 0;

LAB7:    if (t4 != 0)
        goto LAB3;

LAB4:    t14 = (t0 + 1992U);
    t15 = *((char **)t14);
    t14 = (t0 + 6186);
    t17 = 1;
    if (3U == 3U)
        goto LAB13;

LAB14:    t17 = 0;

LAB15:    if (t17 != 0)
        goto LAB11;

LAB12:    t27 = (t0 + 1992U);
    t28 = *((char **)t27);
    t27 = (t0 + 6189);
    t30 = 1;
    if (3U == 3U)
        goto LAB21;

LAB22:    t30 = 0;

LAB23:    if (t30 != 0)
        goto LAB19;

LAB20:    t40 = (t0 + 1992U);
    t41 = *((char **)t40);
    t40 = (t0 + 6192);
    t43 = 1;
    if (3U == 3U)
        goto LAB29;

LAB30:    t43 = 0;

LAB31:    if (t43 != 0)
        goto LAB27;

LAB28:    t53 = (t0 + 1992U);
    t54 = *((char **)t53);
    t53 = (t0 + 6195);
    t56 = 1;
    if (3U == 3U)
        goto LAB37;

LAB38:    t56 = 0;

LAB39:    if (t56 != 0)
        goto LAB35;

LAB36:
LAB43:    t66 = (t0 + 1832U);
    t67 = *((char **)t66);
    t66 = (t0 + 3712);
    t68 = (t66 + 56U);
    t69 = *((char **)t68);
    t70 = (t69 + 56U);
    t71 = *((char **)t70);
    memcpy(t71, t67, 32U);
    xsi_driver_first_trans_fast_port(t66);

LAB2:    t72 = (t0 + 3632);
    *((int *)t72) = 1;

LAB1:    return;
LAB3:    t8 = (t0 + 1032U);
    t9 = *((char **)t8);
    t8 = (t0 + 3712);
    t10 = (t8 + 56U);
    t11 = *((char **)t10);
    t12 = (t11 + 56U);
    t13 = *((char **)t12);
    memcpy(t13, t9, 32U);
    xsi_driver_first_trans_fast_port(t8);
    goto LAB2;

LAB5:    t5 = 0;

LAB8:    if (t5 < 3U)
        goto LAB9;
    else
        goto LAB7;

LAB9:    t6 = (t2 + t5);
    t7 = (t1 + t5);
    if (*((unsigned char *)t6) != *((unsigned char *)t7))
        goto LAB6;

LAB10:    t5 = (t5 + 1);
    goto LAB8;

LAB11:    t21 = (t0 + 1192U);
    t22 = *((char **)t21);
    t21 = (t0 + 3712);
    t23 = (t21 + 56U);
    t24 = *((char **)t23);
    t25 = (t24 + 56U);
    t26 = *((char **)t25);
    memcpy(t26, t22, 32U);
    xsi_driver_first_trans_fast_port(t21);
    goto LAB2;

LAB13:    t18 = 0;

LAB16:    if (t18 < 3U)
        goto LAB17;
    else
        goto LAB15;

LAB17:    t19 = (t15 + t18);
    t20 = (t14 + t18);
    if (*((unsigned char *)t19) != *((unsigned char *)t20))
        goto LAB14;

LAB18:    t18 = (t18 + 1);
    goto LAB16;

LAB19:    t34 = (t0 + 1352U);
    t35 = *((char **)t34);
    t34 = (t0 + 3712);
    t36 = (t34 + 56U);
    t37 = *((char **)t36);
    t38 = (t37 + 56U);
    t39 = *((char **)t38);
    memcpy(t39, t35, 32U);
    xsi_driver_first_trans_fast_port(t34);
    goto LAB2;

LAB21:    t31 = 0;

LAB24:    if (t31 < 3U)
        goto LAB25;
    else
        goto LAB23;

LAB25:    t32 = (t28 + t31);
    t33 = (t27 + t31);
    if (*((unsigned char *)t32) != *((unsigned char *)t33))
        goto LAB22;

LAB26:    t31 = (t31 + 1);
    goto LAB24;

LAB27:    t47 = (t0 + 1512U);
    t48 = *((char **)t47);
    t47 = (t0 + 3712);
    t49 = (t47 + 56U);
    t50 = *((char **)t49);
    t51 = (t50 + 56U);
    t52 = *((char **)t51);
    memcpy(t52, t48, 32U);
    xsi_driver_first_trans_fast_port(t47);
    goto LAB2;

LAB29:    t44 = 0;

LAB32:    if (t44 < 3U)
        goto LAB33;
    else
        goto LAB31;

LAB33:    t45 = (t41 + t44);
    t46 = (t40 + t44);
    if (*((unsigned char *)t45) != *((unsigned char *)t46))
        goto LAB30;

LAB34:    t44 = (t44 + 1);
    goto LAB32;

LAB35:    t60 = (t0 + 1672U);
    t61 = *((char **)t60);
    t60 = (t0 + 3712);
    t62 = (t60 + 56U);
    t63 = *((char **)t62);
    t64 = (t63 + 56U);
    t65 = *((char **)t64);
    memcpy(t65, t61, 32U);
    xsi_driver_first_trans_fast_port(t60);
    goto LAB2;

LAB37:    t57 = 0;

LAB40:    if (t57 < 3U)
        goto LAB41;
    else
        goto LAB39;

LAB41:    t58 = (t54 + t57);
    t59 = (t53 + t57);
    if (*((unsigned char *)t58) != *((unsigned char *)t59))
        goto LAB38;

LAB42:    t57 = (t57 + 1);
    goto LAB40;

LAB44:    goto LAB2;

}


extern void work_a_1457497372_3212880686_init()
{
	static char *pe[] = {(void *)work_a_1457497372_3212880686_p_0};
	xsi_register_didat("work_a_1457497372_3212880686", "isim/AtlasTest_isim_beh.exe.sim/work/a_1457497372_3212880686.didat");
	xsi_register_executes(pe);
}
