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
static const char *ng0 = "C:/Users/Matthew/Desktop/ALU/BShifter.vhd";
extern char *IEEE_P_2592010699;

unsigned char ieee_p_2592010699_sub_1605435078_503743352(char *, unsigned char , unsigned char );


static void work_a_3788663585_4261049609_p_0(char *t0)
{
    char *t1;
    char *t2;
    unsigned char t3;
    unsigned char t4;
    unsigned char t5;
    char *t6;
    int t7;
    char *t8;
    int t9;
    unsigned char t10;
    char *t11;
    unsigned char t12;
    unsigned char t13;
    char *t14;
    char *t15;
    int t16;
    char *t17;
    int t18;
    int t19;
    int t20;
    int t21;
    unsigned int t22;
    unsigned int t23;
    unsigned int t24;
    unsigned char t25;
    char *t26;
    char *t27;
    char *t28;
    char *t29;
    char *t30;
    int t31;
    int t32;

LAB0:    xsi_set_current_line(23, ng0);
    t1 = (t0 + 3464U);
    t2 = *((char **)t1);
    t3 = *((unsigned char *)t2);
    t4 = (t3 == (unsigned char)3);
    if (t4 != 0)
        goto LAB2;

LAB4:    xsi_set_current_line(30, ng0);
    t1 = (t0 + 4360U);
    t2 = *((char **)t1);
    t7 = *((int *)t2);
    t1 = (t0 + 3304U);
    t6 = *((char **)t1);
    t9 = *((int *)t6);
    t16 = (8 - t9);
    t18 = (t16 - 1);
    t4 = (t7 <= t18);
    if (t4 == 1)
        goto LAB14;

LAB15:    t1 = (t0 + 3624U);
    t8 = *((char **)t1);
    t5 = *((unsigned char *)t8);
    t10 = (t5 == (unsigned char)3);
    t3 = t10;

LAB16:    if (t3 != 0)
        goto LAB11;

LAB13:    xsi_set_current_line(33, ng0);
    t1 = (t0 + 3784U);
    t2 = *((char **)t1);
    t3 = *((unsigned char *)t2);
    t1 = (t0 + 3144U);
    t6 = *((char **)t1);
    t7 = (8 - 1);
    t9 = (t7 - 7);
    t22 = (t9 * -1);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t6 + t24);
    t4 = *((unsigned char *)t1);
    t5 = ieee_p_2592010699_sub_1605435078_503743352(IEEE_P_2592010699, t3, t4);
    t8 = (t0 + 8432);
    t11 = (t8 + 56U);
    t14 = *((char **)t11);
    t15 = (t14 + 56U);
    t17 = *((char **)t15);
    *((unsigned char *)t17) = t5;
    xsi_driver_first_trans_delta(t8, 0U, 1, 0LL);

LAB12:
LAB3:    t1 = (t0 + 8240);
    *((int *)t1) = 1;

LAB1:    return;
LAB2:    xsi_set_current_line(24, ng0);
    t1 = (t0 + 4360U);
    t6 = *((char **)t1);
    t7 = *((int *)t6);
    t1 = (t0 + 3304U);
    t8 = *((char **)t1);
    t9 = *((int *)t8);
    t10 = (t7 >= t9);
    if (t10 == 1)
        goto LAB8;

LAB9:    t1 = (t0 + 3624U);
    t11 = *((char **)t1);
    t12 = *((unsigned char *)t11);
    t13 = (t12 == (unsigned char)3);
    t5 = t13;

LAB10:    if (t5 != 0)
        goto LAB5;

LAB7:    xsi_set_current_line(27, ng0);
    t1 = (t0 + 8432);
    t2 = (t1 + 56U);
    t6 = *((char **)t2);
    t8 = (t6 + 56U);
    t11 = *((char **)t8);
    *((unsigned char *)t11) = (unsigned char)2;
    xsi_driver_first_trans_delta(t1, 0U, 1, 0LL);

LAB6:    goto LAB3;

LAB5:    xsi_set_current_line(25, ng0);
    t1 = (t0 + 3144U);
    t14 = *((char **)t1);
    t1 = (t0 + 4360U);
    t15 = *((char **)t1);
    t16 = *((int *)t15);
    t1 = (t0 + 3304U);
    t17 = *((char **)t1);
    t18 = *((int *)t17);
    t19 = (t16 - t18);
    t20 = xsi_vhdl_mod(t19, 8);
    t21 = (t20 - 7);
    t22 = (t21 * -1);
    xsi_vhdl_check_range_of_index(7, 0, -1, t20);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t14 + t24);
    t25 = *((unsigned char *)t1);
    t26 = (t0 + 8432);
    t27 = (t26 + 56U);
    t28 = *((char **)t27);
    t29 = (t28 + 56U);
    t30 = *((char **)t29);
    *((unsigned char *)t30) = t25;
    xsi_driver_first_trans_delta(t26, 0U, 1, 0LL);
    goto LAB6;

LAB8:    t5 = (unsigned char)1;
    goto LAB10;

LAB11:    xsi_set_current_line(31, ng0);
    t1 = (t0 + 3144U);
    t11 = *((char **)t1);
    t1 = (t0 + 4360U);
    t14 = *((char **)t1);
    t19 = *((int *)t14);
    t1 = (t0 + 3304U);
    t15 = *((char **)t1);
    t20 = *((int *)t15);
    t21 = (t19 + t20);
    t31 = xsi_vhdl_mod(t21, 8);
    t32 = (t31 - 7);
    t22 = (t32 * -1);
    xsi_vhdl_check_range_of_index(7, 0, -1, t31);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t11 + t24);
    t12 = *((unsigned char *)t1);
    t17 = (t0 + 8432);
    t26 = (t17 + 56U);
    t27 = *((char **)t26);
    t28 = (t27 + 56U);
    t29 = *((char **)t28);
    *((unsigned char *)t29) = t12;
    xsi_driver_first_trans_delta(t17, 0U, 1, 0LL);
    goto LAB12;

LAB14:    t3 = (unsigned char)1;
    goto LAB16;

}

static void work_a_3788663585_4261049609_p_1(char *t0)
{
    char *t1;
    char *t2;
    unsigned char t3;
    unsigned char t4;
    unsigned char t5;
    char *t6;
    int t7;
    char *t8;
    int t9;
    unsigned char t10;
    char *t11;
    unsigned char t12;
    unsigned char t13;
    char *t14;
    char *t15;
    int t16;
    char *t17;
    int t18;
    int t19;
    int t20;
    int t21;
    unsigned int t22;
    unsigned int t23;
    unsigned int t24;
    unsigned char t25;
    char *t26;
    char *t27;
    char *t28;
    char *t29;
    char *t30;
    int t31;
    int t32;

LAB0:    xsi_set_current_line(23, ng0);
    t1 = (t0 + 3464U);
    t2 = *((char **)t1);
    t3 = *((unsigned char *)t2);
    t4 = (t3 == (unsigned char)3);
    if (t4 != 0)
        goto LAB2;

LAB4:    xsi_set_current_line(30, ng0);
    t1 = (t0 + 4480U);
    t2 = *((char **)t1);
    t7 = *((int *)t2);
    t1 = (t0 + 3304U);
    t6 = *((char **)t1);
    t9 = *((int *)t6);
    t16 = (8 - t9);
    t18 = (t16 - 1);
    t4 = (t7 <= t18);
    if (t4 == 1)
        goto LAB14;

LAB15:    t1 = (t0 + 3624U);
    t8 = *((char **)t1);
    t5 = *((unsigned char *)t8);
    t10 = (t5 == (unsigned char)3);
    t3 = t10;

LAB16:    if (t3 != 0)
        goto LAB11;

LAB13:    xsi_set_current_line(33, ng0);
    t1 = (t0 + 3784U);
    t2 = *((char **)t1);
    t3 = *((unsigned char *)t2);
    t1 = (t0 + 3144U);
    t6 = *((char **)t1);
    t7 = (8 - 1);
    t9 = (t7 - 7);
    t22 = (t9 * -1);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t6 + t24);
    t4 = *((unsigned char *)t1);
    t5 = ieee_p_2592010699_sub_1605435078_503743352(IEEE_P_2592010699, t3, t4);
    t8 = (t0 + 8496);
    t11 = (t8 + 56U);
    t14 = *((char **)t11);
    t15 = (t14 + 56U);
    t17 = *((char **)t15);
    *((unsigned char *)t17) = t5;
    xsi_driver_first_trans_delta(t8, 1U, 1, 0LL);

LAB12:
LAB3:    t1 = (t0 + 8256);
    *((int *)t1) = 1;

LAB1:    return;
LAB2:    xsi_set_current_line(24, ng0);
    t1 = (t0 + 4480U);
    t6 = *((char **)t1);
    t7 = *((int *)t6);
    t1 = (t0 + 3304U);
    t8 = *((char **)t1);
    t9 = *((int *)t8);
    t10 = (t7 >= t9);
    if (t10 == 1)
        goto LAB8;

LAB9:    t1 = (t0 + 3624U);
    t11 = *((char **)t1);
    t12 = *((unsigned char *)t11);
    t13 = (t12 == (unsigned char)3);
    t5 = t13;

LAB10:    if (t5 != 0)
        goto LAB5;

LAB7:    xsi_set_current_line(27, ng0);
    t1 = (t0 + 8496);
    t2 = (t1 + 56U);
    t6 = *((char **)t2);
    t8 = (t6 + 56U);
    t11 = *((char **)t8);
    *((unsigned char *)t11) = (unsigned char)2;
    xsi_driver_first_trans_delta(t1, 1U, 1, 0LL);

LAB6:    goto LAB3;

LAB5:    xsi_set_current_line(25, ng0);
    t1 = (t0 + 3144U);
    t14 = *((char **)t1);
    t1 = (t0 + 4480U);
    t15 = *((char **)t1);
    t16 = *((int *)t15);
    t1 = (t0 + 3304U);
    t17 = *((char **)t1);
    t18 = *((int *)t17);
    t19 = (t16 - t18);
    t20 = xsi_vhdl_mod(t19, 8);
    t21 = (t20 - 7);
    t22 = (t21 * -1);
    xsi_vhdl_check_range_of_index(7, 0, -1, t20);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t14 + t24);
    t25 = *((unsigned char *)t1);
    t26 = (t0 + 8496);
    t27 = (t26 + 56U);
    t28 = *((char **)t27);
    t29 = (t28 + 56U);
    t30 = *((char **)t29);
    *((unsigned char *)t30) = t25;
    xsi_driver_first_trans_delta(t26, 1U, 1, 0LL);
    goto LAB6;

LAB8:    t5 = (unsigned char)1;
    goto LAB10;

LAB11:    xsi_set_current_line(31, ng0);
    t1 = (t0 + 3144U);
    t11 = *((char **)t1);
    t1 = (t0 + 4480U);
    t14 = *((char **)t1);
    t19 = *((int *)t14);
    t1 = (t0 + 3304U);
    t15 = *((char **)t1);
    t20 = *((int *)t15);
    t21 = (t19 + t20);
    t31 = xsi_vhdl_mod(t21, 8);
    t32 = (t31 - 7);
    t22 = (t32 * -1);
    xsi_vhdl_check_range_of_index(7, 0, -1, t31);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t11 + t24);
    t12 = *((unsigned char *)t1);
    t17 = (t0 + 8496);
    t26 = (t17 + 56U);
    t27 = *((char **)t26);
    t28 = (t27 + 56U);
    t29 = *((char **)t28);
    *((unsigned char *)t29) = t12;
    xsi_driver_first_trans_delta(t17, 1U, 1, 0LL);
    goto LAB12;

LAB14:    t3 = (unsigned char)1;
    goto LAB16;

}

static void work_a_3788663585_4261049609_p_2(char *t0)
{
    char *t1;
    char *t2;
    unsigned char t3;
    unsigned char t4;
    unsigned char t5;
    char *t6;
    int t7;
    char *t8;
    int t9;
    unsigned char t10;
    char *t11;
    unsigned char t12;
    unsigned char t13;
    char *t14;
    char *t15;
    int t16;
    char *t17;
    int t18;
    int t19;
    int t20;
    int t21;
    unsigned int t22;
    unsigned int t23;
    unsigned int t24;
    unsigned char t25;
    char *t26;
    char *t27;
    char *t28;
    char *t29;
    char *t30;
    int t31;
    int t32;

LAB0:    xsi_set_current_line(23, ng0);
    t1 = (t0 + 3464U);
    t2 = *((char **)t1);
    t3 = *((unsigned char *)t2);
    t4 = (t3 == (unsigned char)3);
    if (t4 != 0)
        goto LAB2;

LAB4:    xsi_set_current_line(30, ng0);
    t1 = (t0 + 4600U);
    t2 = *((char **)t1);
    t7 = *((int *)t2);
    t1 = (t0 + 3304U);
    t6 = *((char **)t1);
    t9 = *((int *)t6);
    t16 = (8 - t9);
    t18 = (t16 - 1);
    t4 = (t7 <= t18);
    if (t4 == 1)
        goto LAB14;

LAB15:    t1 = (t0 + 3624U);
    t8 = *((char **)t1);
    t5 = *((unsigned char *)t8);
    t10 = (t5 == (unsigned char)3);
    t3 = t10;

LAB16:    if (t3 != 0)
        goto LAB11;

LAB13:    xsi_set_current_line(33, ng0);
    t1 = (t0 + 3784U);
    t2 = *((char **)t1);
    t3 = *((unsigned char *)t2);
    t1 = (t0 + 3144U);
    t6 = *((char **)t1);
    t7 = (8 - 1);
    t9 = (t7 - 7);
    t22 = (t9 * -1);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t6 + t24);
    t4 = *((unsigned char *)t1);
    t5 = ieee_p_2592010699_sub_1605435078_503743352(IEEE_P_2592010699, t3, t4);
    t8 = (t0 + 8560);
    t11 = (t8 + 56U);
    t14 = *((char **)t11);
    t15 = (t14 + 56U);
    t17 = *((char **)t15);
    *((unsigned char *)t17) = t5;
    xsi_driver_first_trans_delta(t8, 2U, 1, 0LL);

LAB12:
LAB3:    t1 = (t0 + 8272);
    *((int *)t1) = 1;

LAB1:    return;
LAB2:    xsi_set_current_line(24, ng0);
    t1 = (t0 + 4600U);
    t6 = *((char **)t1);
    t7 = *((int *)t6);
    t1 = (t0 + 3304U);
    t8 = *((char **)t1);
    t9 = *((int *)t8);
    t10 = (t7 >= t9);
    if (t10 == 1)
        goto LAB8;

LAB9:    t1 = (t0 + 3624U);
    t11 = *((char **)t1);
    t12 = *((unsigned char *)t11);
    t13 = (t12 == (unsigned char)3);
    t5 = t13;

LAB10:    if (t5 != 0)
        goto LAB5;

LAB7:    xsi_set_current_line(27, ng0);
    t1 = (t0 + 8560);
    t2 = (t1 + 56U);
    t6 = *((char **)t2);
    t8 = (t6 + 56U);
    t11 = *((char **)t8);
    *((unsigned char *)t11) = (unsigned char)2;
    xsi_driver_first_trans_delta(t1, 2U, 1, 0LL);

LAB6:    goto LAB3;

LAB5:    xsi_set_current_line(25, ng0);
    t1 = (t0 + 3144U);
    t14 = *((char **)t1);
    t1 = (t0 + 4600U);
    t15 = *((char **)t1);
    t16 = *((int *)t15);
    t1 = (t0 + 3304U);
    t17 = *((char **)t1);
    t18 = *((int *)t17);
    t19 = (t16 - t18);
    t20 = xsi_vhdl_mod(t19, 8);
    t21 = (t20 - 7);
    t22 = (t21 * -1);
    xsi_vhdl_check_range_of_index(7, 0, -1, t20);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t14 + t24);
    t25 = *((unsigned char *)t1);
    t26 = (t0 + 8560);
    t27 = (t26 + 56U);
    t28 = *((char **)t27);
    t29 = (t28 + 56U);
    t30 = *((char **)t29);
    *((unsigned char *)t30) = t25;
    xsi_driver_first_trans_delta(t26, 2U, 1, 0LL);
    goto LAB6;

LAB8:    t5 = (unsigned char)1;
    goto LAB10;

LAB11:    xsi_set_current_line(31, ng0);
    t1 = (t0 + 3144U);
    t11 = *((char **)t1);
    t1 = (t0 + 4600U);
    t14 = *((char **)t1);
    t19 = *((int *)t14);
    t1 = (t0 + 3304U);
    t15 = *((char **)t1);
    t20 = *((int *)t15);
    t21 = (t19 + t20);
    t31 = xsi_vhdl_mod(t21, 8);
    t32 = (t31 - 7);
    t22 = (t32 * -1);
    xsi_vhdl_check_range_of_index(7, 0, -1, t31);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t11 + t24);
    t12 = *((unsigned char *)t1);
    t17 = (t0 + 8560);
    t26 = (t17 + 56U);
    t27 = *((char **)t26);
    t28 = (t27 + 56U);
    t29 = *((char **)t28);
    *((unsigned char *)t29) = t12;
    xsi_driver_first_trans_delta(t17, 2U, 1, 0LL);
    goto LAB12;

LAB14:    t3 = (unsigned char)1;
    goto LAB16;

}

static void work_a_3788663585_4261049609_p_3(char *t0)
{
    char *t1;
    char *t2;
    unsigned char t3;
    unsigned char t4;
    unsigned char t5;
    char *t6;
    int t7;
    char *t8;
    int t9;
    unsigned char t10;
    char *t11;
    unsigned char t12;
    unsigned char t13;
    char *t14;
    char *t15;
    int t16;
    char *t17;
    int t18;
    int t19;
    int t20;
    int t21;
    unsigned int t22;
    unsigned int t23;
    unsigned int t24;
    unsigned char t25;
    char *t26;
    char *t27;
    char *t28;
    char *t29;
    char *t30;
    int t31;
    int t32;

LAB0:    xsi_set_current_line(23, ng0);
    t1 = (t0 + 3464U);
    t2 = *((char **)t1);
    t3 = *((unsigned char *)t2);
    t4 = (t3 == (unsigned char)3);
    if (t4 != 0)
        goto LAB2;

LAB4:    xsi_set_current_line(30, ng0);
    t1 = (t0 + 4720U);
    t2 = *((char **)t1);
    t7 = *((int *)t2);
    t1 = (t0 + 3304U);
    t6 = *((char **)t1);
    t9 = *((int *)t6);
    t16 = (8 - t9);
    t18 = (t16 - 1);
    t4 = (t7 <= t18);
    if (t4 == 1)
        goto LAB14;

LAB15:    t1 = (t0 + 3624U);
    t8 = *((char **)t1);
    t5 = *((unsigned char *)t8);
    t10 = (t5 == (unsigned char)3);
    t3 = t10;

LAB16:    if (t3 != 0)
        goto LAB11;

LAB13:    xsi_set_current_line(33, ng0);
    t1 = (t0 + 3784U);
    t2 = *((char **)t1);
    t3 = *((unsigned char *)t2);
    t1 = (t0 + 3144U);
    t6 = *((char **)t1);
    t7 = (8 - 1);
    t9 = (t7 - 7);
    t22 = (t9 * -1);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t6 + t24);
    t4 = *((unsigned char *)t1);
    t5 = ieee_p_2592010699_sub_1605435078_503743352(IEEE_P_2592010699, t3, t4);
    t8 = (t0 + 8624);
    t11 = (t8 + 56U);
    t14 = *((char **)t11);
    t15 = (t14 + 56U);
    t17 = *((char **)t15);
    *((unsigned char *)t17) = t5;
    xsi_driver_first_trans_delta(t8, 3U, 1, 0LL);

LAB12:
LAB3:    t1 = (t0 + 8288);
    *((int *)t1) = 1;

LAB1:    return;
LAB2:    xsi_set_current_line(24, ng0);
    t1 = (t0 + 4720U);
    t6 = *((char **)t1);
    t7 = *((int *)t6);
    t1 = (t0 + 3304U);
    t8 = *((char **)t1);
    t9 = *((int *)t8);
    t10 = (t7 >= t9);
    if (t10 == 1)
        goto LAB8;

LAB9:    t1 = (t0 + 3624U);
    t11 = *((char **)t1);
    t12 = *((unsigned char *)t11);
    t13 = (t12 == (unsigned char)3);
    t5 = t13;

LAB10:    if (t5 != 0)
        goto LAB5;

LAB7:    xsi_set_current_line(27, ng0);
    t1 = (t0 + 8624);
    t2 = (t1 + 56U);
    t6 = *((char **)t2);
    t8 = (t6 + 56U);
    t11 = *((char **)t8);
    *((unsigned char *)t11) = (unsigned char)2;
    xsi_driver_first_trans_delta(t1, 3U, 1, 0LL);

LAB6:    goto LAB3;

LAB5:    xsi_set_current_line(25, ng0);
    t1 = (t0 + 3144U);
    t14 = *((char **)t1);
    t1 = (t0 + 4720U);
    t15 = *((char **)t1);
    t16 = *((int *)t15);
    t1 = (t0 + 3304U);
    t17 = *((char **)t1);
    t18 = *((int *)t17);
    t19 = (t16 - t18);
    t20 = xsi_vhdl_mod(t19, 8);
    t21 = (t20 - 7);
    t22 = (t21 * -1);
    xsi_vhdl_check_range_of_index(7, 0, -1, t20);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t14 + t24);
    t25 = *((unsigned char *)t1);
    t26 = (t0 + 8624);
    t27 = (t26 + 56U);
    t28 = *((char **)t27);
    t29 = (t28 + 56U);
    t30 = *((char **)t29);
    *((unsigned char *)t30) = t25;
    xsi_driver_first_trans_delta(t26, 3U, 1, 0LL);
    goto LAB6;

LAB8:    t5 = (unsigned char)1;
    goto LAB10;

LAB11:    xsi_set_current_line(31, ng0);
    t1 = (t0 + 3144U);
    t11 = *((char **)t1);
    t1 = (t0 + 4720U);
    t14 = *((char **)t1);
    t19 = *((int *)t14);
    t1 = (t0 + 3304U);
    t15 = *((char **)t1);
    t20 = *((int *)t15);
    t21 = (t19 + t20);
    t31 = xsi_vhdl_mod(t21, 8);
    t32 = (t31 - 7);
    t22 = (t32 * -1);
    xsi_vhdl_check_range_of_index(7, 0, -1, t31);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t11 + t24);
    t12 = *((unsigned char *)t1);
    t17 = (t0 + 8624);
    t26 = (t17 + 56U);
    t27 = *((char **)t26);
    t28 = (t27 + 56U);
    t29 = *((char **)t28);
    *((unsigned char *)t29) = t12;
    xsi_driver_first_trans_delta(t17, 3U, 1, 0LL);
    goto LAB12;

LAB14:    t3 = (unsigned char)1;
    goto LAB16;

}

static void work_a_3788663585_4261049609_p_4(char *t0)
{
    char *t1;
    char *t2;
    unsigned char t3;
    unsigned char t4;
    unsigned char t5;
    char *t6;
    int t7;
    char *t8;
    int t9;
    unsigned char t10;
    char *t11;
    unsigned char t12;
    unsigned char t13;
    char *t14;
    char *t15;
    int t16;
    char *t17;
    int t18;
    int t19;
    int t20;
    int t21;
    unsigned int t22;
    unsigned int t23;
    unsigned int t24;
    unsigned char t25;
    char *t26;
    char *t27;
    char *t28;
    char *t29;
    char *t30;
    int t31;
    int t32;

LAB0:    xsi_set_current_line(23, ng0);
    t1 = (t0 + 3464U);
    t2 = *((char **)t1);
    t3 = *((unsigned char *)t2);
    t4 = (t3 == (unsigned char)3);
    if (t4 != 0)
        goto LAB2;

LAB4:    xsi_set_current_line(30, ng0);
    t1 = (t0 + 4840U);
    t2 = *((char **)t1);
    t7 = *((int *)t2);
    t1 = (t0 + 3304U);
    t6 = *((char **)t1);
    t9 = *((int *)t6);
    t16 = (8 - t9);
    t18 = (t16 - 1);
    t4 = (t7 <= t18);
    if (t4 == 1)
        goto LAB14;

LAB15:    t1 = (t0 + 3624U);
    t8 = *((char **)t1);
    t5 = *((unsigned char *)t8);
    t10 = (t5 == (unsigned char)3);
    t3 = t10;

LAB16:    if (t3 != 0)
        goto LAB11;

LAB13:    xsi_set_current_line(33, ng0);
    t1 = (t0 + 3784U);
    t2 = *((char **)t1);
    t3 = *((unsigned char *)t2);
    t1 = (t0 + 3144U);
    t6 = *((char **)t1);
    t7 = (8 - 1);
    t9 = (t7 - 7);
    t22 = (t9 * -1);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t6 + t24);
    t4 = *((unsigned char *)t1);
    t5 = ieee_p_2592010699_sub_1605435078_503743352(IEEE_P_2592010699, t3, t4);
    t8 = (t0 + 8688);
    t11 = (t8 + 56U);
    t14 = *((char **)t11);
    t15 = (t14 + 56U);
    t17 = *((char **)t15);
    *((unsigned char *)t17) = t5;
    xsi_driver_first_trans_delta(t8, 4U, 1, 0LL);

LAB12:
LAB3:    t1 = (t0 + 8304);
    *((int *)t1) = 1;

LAB1:    return;
LAB2:    xsi_set_current_line(24, ng0);
    t1 = (t0 + 4840U);
    t6 = *((char **)t1);
    t7 = *((int *)t6);
    t1 = (t0 + 3304U);
    t8 = *((char **)t1);
    t9 = *((int *)t8);
    t10 = (t7 >= t9);
    if (t10 == 1)
        goto LAB8;

LAB9:    t1 = (t0 + 3624U);
    t11 = *((char **)t1);
    t12 = *((unsigned char *)t11);
    t13 = (t12 == (unsigned char)3);
    t5 = t13;

LAB10:    if (t5 != 0)
        goto LAB5;

LAB7:    xsi_set_current_line(27, ng0);
    t1 = (t0 + 8688);
    t2 = (t1 + 56U);
    t6 = *((char **)t2);
    t8 = (t6 + 56U);
    t11 = *((char **)t8);
    *((unsigned char *)t11) = (unsigned char)2;
    xsi_driver_first_trans_delta(t1, 4U, 1, 0LL);

LAB6:    goto LAB3;

LAB5:    xsi_set_current_line(25, ng0);
    t1 = (t0 + 3144U);
    t14 = *((char **)t1);
    t1 = (t0 + 4840U);
    t15 = *((char **)t1);
    t16 = *((int *)t15);
    t1 = (t0 + 3304U);
    t17 = *((char **)t1);
    t18 = *((int *)t17);
    t19 = (t16 - t18);
    t20 = xsi_vhdl_mod(t19, 8);
    t21 = (t20 - 7);
    t22 = (t21 * -1);
    xsi_vhdl_check_range_of_index(7, 0, -1, t20);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t14 + t24);
    t25 = *((unsigned char *)t1);
    t26 = (t0 + 8688);
    t27 = (t26 + 56U);
    t28 = *((char **)t27);
    t29 = (t28 + 56U);
    t30 = *((char **)t29);
    *((unsigned char *)t30) = t25;
    xsi_driver_first_trans_delta(t26, 4U, 1, 0LL);
    goto LAB6;

LAB8:    t5 = (unsigned char)1;
    goto LAB10;

LAB11:    xsi_set_current_line(31, ng0);
    t1 = (t0 + 3144U);
    t11 = *((char **)t1);
    t1 = (t0 + 4840U);
    t14 = *((char **)t1);
    t19 = *((int *)t14);
    t1 = (t0 + 3304U);
    t15 = *((char **)t1);
    t20 = *((int *)t15);
    t21 = (t19 + t20);
    t31 = xsi_vhdl_mod(t21, 8);
    t32 = (t31 - 7);
    t22 = (t32 * -1);
    xsi_vhdl_check_range_of_index(7, 0, -1, t31);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t11 + t24);
    t12 = *((unsigned char *)t1);
    t17 = (t0 + 8688);
    t26 = (t17 + 56U);
    t27 = *((char **)t26);
    t28 = (t27 + 56U);
    t29 = *((char **)t28);
    *((unsigned char *)t29) = t12;
    xsi_driver_first_trans_delta(t17, 4U, 1, 0LL);
    goto LAB12;

LAB14:    t3 = (unsigned char)1;
    goto LAB16;

}

static void work_a_3788663585_4261049609_p_5(char *t0)
{
    char *t1;
    char *t2;
    unsigned char t3;
    unsigned char t4;
    unsigned char t5;
    char *t6;
    int t7;
    char *t8;
    int t9;
    unsigned char t10;
    char *t11;
    unsigned char t12;
    unsigned char t13;
    char *t14;
    char *t15;
    int t16;
    char *t17;
    int t18;
    int t19;
    int t20;
    int t21;
    unsigned int t22;
    unsigned int t23;
    unsigned int t24;
    unsigned char t25;
    char *t26;
    char *t27;
    char *t28;
    char *t29;
    char *t30;
    int t31;
    int t32;

LAB0:    xsi_set_current_line(23, ng0);
    t1 = (t0 + 3464U);
    t2 = *((char **)t1);
    t3 = *((unsigned char *)t2);
    t4 = (t3 == (unsigned char)3);
    if (t4 != 0)
        goto LAB2;

LAB4:    xsi_set_current_line(30, ng0);
    t1 = (t0 + 4960U);
    t2 = *((char **)t1);
    t7 = *((int *)t2);
    t1 = (t0 + 3304U);
    t6 = *((char **)t1);
    t9 = *((int *)t6);
    t16 = (8 - t9);
    t18 = (t16 - 1);
    t4 = (t7 <= t18);
    if (t4 == 1)
        goto LAB14;

LAB15:    t1 = (t0 + 3624U);
    t8 = *((char **)t1);
    t5 = *((unsigned char *)t8);
    t10 = (t5 == (unsigned char)3);
    t3 = t10;

LAB16:    if (t3 != 0)
        goto LAB11;

LAB13:    xsi_set_current_line(33, ng0);
    t1 = (t0 + 3784U);
    t2 = *((char **)t1);
    t3 = *((unsigned char *)t2);
    t1 = (t0 + 3144U);
    t6 = *((char **)t1);
    t7 = (8 - 1);
    t9 = (t7 - 7);
    t22 = (t9 * -1);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t6 + t24);
    t4 = *((unsigned char *)t1);
    t5 = ieee_p_2592010699_sub_1605435078_503743352(IEEE_P_2592010699, t3, t4);
    t8 = (t0 + 8752);
    t11 = (t8 + 56U);
    t14 = *((char **)t11);
    t15 = (t14 + 56U);
    t17 = *((char **)t15);
    *((unsigned char *)t17) = t5;
    xsi_driver_first_trans_delta(t8, 5U, 1, 0LL);

LAB12:
LAB3:    t1 = (t0 + 8320);
    *((int *)t1) = 1;

LAB1:    return;
LAB2:    xsi_set_current_line(24, ng0);
    t1 = (t0 + 4960U);
    t6 = *((char **)t1);
    t7 = *((int *)t6);
    t1 = (t0 + 3304U);
    t8 = *((char **)t1);
    t9 = *((int *)t8);
    t10 = (t7 >= t9);
    if (t10 == 1)
        goto LAB8;

LAB9:    t1 = (t0 + 3624U);
    t11 = *((char **)t1);
    t12 = *((unsigned char *)t11);
    t13 = (t12 == (unsigned char)3);
    t5 = t13;

LAB10:    if (t5 != 0)
        goto LAB5;

LAB7:    xsi_set_current_line(27, ng0);
    t1 = (t0 + 8752);
    t2 = (t1 + 56U);
    t6 = *((char **)t2);
    t8 = (t6 + 56U);
    t11 = *((char **)t8);
    *((unsigned char *)t11) = (unsigned char)2;
    xsi_driver_first_trans_delta(t1, 5U, 1, 0LL);

LAB6:    goto LAB3;

LAB5:    xsi_set_current_line(25, ng0);
    t1 = (t0 + 3144U);
    t14 = *((char **)t1);
    t1 = (t0 + 4960U);
    t15 = *((char **)t1);
    t16 = *((int *)t15);
    t1 = (t0 + 3304U);
    t17 = *((char **)t1);
    t18 = *((int *)t17);
    t19 = (t16 - t18);
    t20 = xsi_vhdl_mod(t19, 8);
    t21 = (t20 - 7);
    t22 = (t21 * -1);
    xsi_vhdl_check_range_of_index(7, 0, -1, t20);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t14 + t24);
    t25 = *((unsigned char *)t1);
    t26 = (t0 + 8752);
    t27 = (t26 + 56U);
    t28 = *((char **)t27);
    t29 = (t28 + 56U);
    t30 = *((char **)t29);
    *((unsigned char *)t30) = t25;
    xsi_driver_first_trans_delta(t26, 5U, 1, 0LL);
    goto LAB6;

LAB8:    t5 = (unsigned char)1;
    goto LAB10;

LAB11:    xsi_set_current_line(31, ng0);
    t1 = (t0 + 3144U);
    t11 = *((char **)t1);
    t1 = (t0 + 4960U);
    t14 = *((char **)t1);
    t19 = *((int *)t14);
    t1 = (t0 + 3304U);
    t15 = *((char **)t1);
    t20 = *((int *)t15);
    t21 = (t19 + t20);
    t31 = xsi_vhdl_mod(t21, 8);
    t32 = (t31 - 7);
    t22 = (t32 * -1);
    xsi_vhdl_check_range_of_index(7, 0, -1, t31);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t11 + t24);
    t12 = *((unsigned char *)t1);
    t17 = (t0 + 8752);
    t26 = (t17 + 56U);
    t27 = *((char **)t26);
    t28 = (t27 + 56U);
    t29 = *((char **)t28);
    *((unsigned char *)t29) = t12;
    xsi_driver_first_trans_delta(t17, 5U, 1, 0LL);
    goto LAB12;

LAB14:    t3 = (unsigned char)1;
    goto LAB16;

}

static void work_a_3788663585_4261049609_p_6(char *t0)
{
    char *t1;
    char *t2;
    unsigned char t3;
    unsigned char t4;
    unsigned char t5;
    char *t6;
    int t7;
    char *t8;
    int t9;
    unsigned char t10;
    char *t11;
    unsigned char t12;
    unsigned char t13;
    char *t14;
    char *t15;
    int t16;
    char *t17;
    int t18;
    int t19;
    int t20;
    int t21;
    unsigned int t22;
    unsigned int t23;
    unsigned int t24;
    unsigned char t25;
    char *t26;
    char *t27;
    char *t28;
    char *t29;
    char *t30;
    int t31;
    int t32;

LAB0:    xsi_set_current_line(23, ng0);
    t1 = (t0 + 3464U);
    t2 = *((char **)t1);
    t3 = *((unsigned char *)t2);
    t4 = (t3 == (unsigned char)3);
    if (t4 != 0)
        goto LAB2;

LAB4:    xsi_set_current_line(30, ng0);
    t1 = (t0 + 5080U);
    t2 = *((char **)t1);
    t7 = *((int *)t2);
    t1 = (t0 + 3304U);
    t6 = *((char **)t1);
    t9 = *((int *)t6);
    t16 = (8 - t9);
    t18 = (t16 - 1);
    t4 = (t7 <= t18);
    if (t4 == 1)
        goto LAB14;

LAB15:    t1 = (t0 + 3624U);
    t8 = *((char **)t1);
    t5 = *((unsigned char *)t8);
    t10 = (t5 == (unsigned char)3);
    t3 = t10;

LAB16:    if (t3 != 0)
        goto LAB11;

LAB13:    xsi_set_current_line(33, ng0);
    t1 = (t0 + 3784U);
    t2 = *((char **)t1);
    t3 = *((unsigned char *)t2);
    t1 = (t0 + 3144U);
    t6 = *((char **)t1);
    t7 = (8 - 1);
    t9 = (t7 - 7);
    t22 = (t9 * -1);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t6 + t24);
    t4 = *((unsigned char *)t1);
    t5 = ieee_p_2592010699_sub_1605435078_503743352(IEEE_P_2592010699, t3, t4);
    t8 = (t0 + 8816);
    t11 = (t8 + 56U);
    t14 = *((char **)t11);
    t15 = (t14 + 56U);
    t17 = *((char **)t15);
    *((unsigned char *)t17) = t5;
    xsi_driver_first_trans_delta(t8, 6U, 1, 0LL);

LAB12:
LAB3:    t1 = (t0 + 8336);
    *((int *)t1) = 1;

LAB1:    return;
LAB2:    xsi_set_current_line(24, ng0);
    t1 = (t0 + 5080U);
    t6 = *((char **)t1);
    t7 = *((int *)t6);
    t1 = (t0 + 3304U);
    t8 = *((char **)t1);
    t9 = *((int *)t8);
    t10 = (t7 >= t9);
    if (t10 == 1)
        goto LAB8;

LAB9:    t1 = (t0 + 3624U);
    t11 = *((char **)t1);
    t12 = *((unsigned char *)t11);
    t13 = (t12 == (unsigned char)3);
    t5 = t13;

LAB10:    if (t5 != 0)
        goto LAB5;

LAB7:    xsi_set_current_line(27, ng0);
    t1 = (t0 + 8816);
    t2 = (t1 + 56U);
    t6 = *((char **)t2);
    t8 = (t6 + 56U);
    t11 = *((char **)t8);
    *((unsigned char *)t11) = (unsigned char)2;
    xsi_driver_first_trans_delta(t1, 6U, 1, 0LL);

LAB6:    goto LAB3;

LAB5:    xsi_set_current_line(25, ng0);
    t1 = (t0 + 3144U);
    t14 = *((char **)t1);
    t1 = (t0 + 5080U);
    t15 = *((char **)t1);
    t16 = *((int *)t15);
    t1 = (t0 + 3304U);
    t17 = *((char **)t1);
    t18 = *((int *)t17);
    t19 = (t16 - t18);
    t20 = xsi_vhdl_mod(t19, 8);
    t21 = (t20 - 7);
    t22 = (t21 * -1);
    xsi_vhdl_check_range_of_index(7, 0, -1, t20);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t14 + t24);
    t25 = *((unsigned char *)t1);
    t26 = (t0 + 8816);
    t27 = (t26 + 56U);
    t28 = *((char **)t27);
    t29 = (t28 + 56U);
    t30 = *((char **)t29);
    *((unsigned char *)t30) = t25;
    xsi_driver_first_trans_delta(t26, 6U, 1, 0LL);
    goto LAB6;

LAB8:    t5 = (unsigned char)1;
    goto LAB10;

LAB11:    xsi_set_current_line(31, ng0);
    t1 = (t0 + 3144U);
    t11 = *((char **)t1);
    t1 = (t0 + 5080U);
    t14 = *((char **)t1);
    t19 = *((int *)t14);
    t1 = (t0 + 3304U);
    t15 = *((char **)t1);
    t20 = *((int *)t15);
    t21 = (t19 + t20);
    t31 = xsi_vhdl_mod(t21, 8);
    t32 = (t31 - 7);
    t22 = (t32 * -1);
    xsi_vhdl_check_range_of_index(7, 0, -1, t31);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t11 + t24);
    t12 = *((unsigned char *)t1);
    t17 = (t0 + 8816);
    t26 = (t17 + 56U);
    t27 = *((char **)t26);
    t28 = (t27 + 56U);
    t29 = *((char **)t28);
    *((unsigned char *)t29) = t12;
    xsi_driver_first_trans_delta(t17, 6U, 1, 0LL);
    goto LAB12;

LAB14:    t3 = (unsigned char)1;
    goto LAB16;

}

static void work_a_3788663585_4261049609_p_7(char *t0)
{
    char *t1;
    char *t2;
    unsigned char t3;
    unsigned char t4;
    unsigned char t5;
    char *t6;
    int t7;
    char *t8;
    int t9;
    unsigned char t10;
    char *t11;
    unsigned char t12;
    unsigned char t13;
    char *t14;
    char *t15;
    int t16;
    char *t17;
    int t18;
    int t19;
    int t20;
    int t21;
    unsigned int t22;
    unsigned int t23;
    unsigned int t24;
    unsigned char t25;
    char *t26;
    char *t27;
    char *t28;
    char *t29;
    char *t30;
    int t31;
    int t32;

LAB0:    xsi_set_current_line(23, ng0);
    t1 = (t0 + 3464U);
    t2 = *((char **)t1);
    t3 = *((unsigned char *)t2);
    t4 = (t3 == (unsigned char)3);
    if (t4 != 0)
        goto LAB2;

LAB4:    xsi_set_current_line(30, ng0);
    t1 = (t0 + 5200U);
    t2 = *((char **)t1);
    t7 = *((int *)t2);
    t1 = (t0 + 3304U);
    t6 = *((char **)t1);
    t9 = *((int *)t6);
    t16 = (8 - t9);
    t18 = (t16 - 1);
    t4 = (t7 <= t18);
    if (t4 == 1)
        goto LAB14;

LAB15:    t1 = (t0 + 3624U);
    t8 = *((char **)t1);
    t5 = *((unsigned char *)t8);
    t10 = (t5 == (unsigned char)3);
    t3 = t10;

LAB16:    if (t3 != 0)
        goto LAB11;

LAB13:    xsi_set_current_line(33, ng0);
    t1 = (t0 + 3784U);
    t2 = *((char **)t1);
    t3 = *((unsigned char *)t2);
    t1 = (t0 + 3144U);
    t6 = *((char **)t1);
    t7 = (8 - 1);
    t9 = (t7 - 7);
    t22 = (t9 * -1);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t6 + t24);
    t4 = *((unsigned char *)t1);
    t5 = ieee_p_2592010699_sub_1605435078_503743352(IEEE_P_2592010699, t3, t4);
    t8 = (t0 + 8880);
    t11 = (t8 + 56U);
    t14 = *((char **)t11);
    t15 = (t14 + 56U);
    t17 = *((char **)t15);
    *((unsigned char *)t17) = t5;
    xsi_driver_first_trans_delta(t8, 7U, 1, 0LL);

LAB12:
LAB3:    t1 = (t0 + 8352);
    *((int *)t1) = 1;

LAB1:    return;
LAB2:    xsi_set_current_line(24, ng0);
    t1 = (t0 + 5200U);
    t6 = *((char **)t1);
    t7 = *((int *)t6);
    t1 = (t0 + 3304U);
    t8 = *((char **)t1);
    t9 = *((int *)t8);
    t10 = (t7 >= t9);
    if (t10 == 1)
        goto LAB8;

LAB9:    t1 = (t0 + 3624U);
    t11 = *((char **)t1);
    t12 = *((unsigned char *)t11);
    t13 = (t12 == (unsigned char)3);
    t5 = t13;

LAB10:    if (t5 != 0)
        goto LAB5;

LAB7:    xsi_set_current_line(27, ng0);
    t1 = (t0 + 8880);
    t2 = (t1 + 56U);
    t6 = *((char **)t2);
    t8 = (t6 + 56U);
    t11 = *((char **)t8);
    *((unsigned char *)t11) = (unsigned char)2;
    xsi_driver_first_trans_delta(t1, 7U, 1, 0LL);

LAB6:    goto LAB3;

LAB5:    xsi_set_current_line(25, ng0);
    t1 = (t0 + 3144U);
    t14 = *((char **)t1);
    t1 = (t0 + 5200U);
    t15 = *((char **)t1);
    t16 = *((int *)t15);
    t1 = (t0 + 3304U);
    t17 = *((char **)t1);
    t18 = *((int *)t17);
    t19 = (t16 - t18);
    t20 = xsi_vhdl_mod(t19, 8);
    t21 = (t20 - 7);
    t22 = (t21 * -1);
    xsi_vhdl_check_range_of_index(7, 0, -1, t20);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t14 + t24);
    t25 = *((unsigned char *)t1);
    t26 = (t0 + 8880);
    t27 = (t26 + 56U);
    t28 = *((char **)t27);
    t29 = (t28 + 56U);
    t30 = *((char **)t29);
    *((unsigned char *)t30) = t25;
    xsi_driver_first_trans_delta(t26, 7U, 1, 0LL);
    goto LAB6;

LAB8:    t5 = (unsigned char)1;
    goto LAB10;

LAB11:    xsi_set_current_line(31, ng0);
    t1 = (t0 + 3144U);
    t11 = *((char **)t1);
    t1 = (t0 + 5200U);
    t14 = *((char **)t1);
    t19 = *((int *)t14);
    t1 = (t0 + 3304U);
    t15 = *((char **)t1);
    t20 = *((int *)t15);
    t21 = (t19 + t20);
    t31 = xsi_vhdl_mod(t21, 8);
    t32 = (t31 - 7);
    t22 = (t32 * -1);
    xsi_vhdl_check_range_of_index(7, 0, -1, t31);
    t23 = (1U * t22);
    t24 = (0 + t23);
    t1 = (t11 + t24);
    t12 = *((unsigned char *)t1);
    t17 = (t0 + 8880);
    t26 = (t17 + 56U);
    t27 = *((char **)t26);
    t28 = (t27 + 56U);
    t29 = *((char **)t28);
    *((unsigned char *)t29) = t12;
    xsi_driver_first_trans_delta(t17, 7U, 1, 0LL);
    goto LAB12;

LAB14:    t3 = (unsigned char)1;
    goto LAB16;

}


extern void work_a_3788663585_4261049609_init()
{
	static char *pe[] = {(void *)work_a_3788663585_4261049609_p_0,(void *)work_a_3788663585_4261049609_p_1,(void *)work_a_3788663585_4261049609_p_2,(void *)work_a_3788663585_4261049609_p_3,(void *)work_a_3788663585_4261049609_p_4,(void *)work_a_3788663585_4261049609_p_5,(void *)work_a_3788663585_4261049609_p_6,(void *)work_a_3788663585_4261049609_p_7};
	xsi_register_didat("work_a_3788663585_4261049609", "isim/ShifterTest_isim_beh.exe.sim/work/a_3788663585_4261049609.didat");
	xsi_register_executes(pe);
}
