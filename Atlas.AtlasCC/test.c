int somefunction(int x, int y, int w, int z);

int main()
{
	//test expressions
	
	int x;
	x = (true || ((true | false) && (true ^ true & false != true) || 4 < 6)) 
		+ 12 << 7 >> 3 + (2 - 6 + (3 * (4 / 6 * 7 + 3 % 4))) + (int)(4.56676) + (&x) - ~4 - !3 + 4 - sizeof(float)
		+ somefunction(3, 0, 2, 1) * (someObj.foo + (2 - 6 + (3 * (4 / 6 * 7 + 3 % 4)))) - someotherObj->foo + 4
		;

}