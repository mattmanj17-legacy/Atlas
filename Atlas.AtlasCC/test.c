//system calls
//to make a system call, call a function which has the correct parameters
//then call SystemCall. writing to address -1 causes a system call
//system call table
// 1 : print(char* c) --- prints a null terminated string to the console;

void SystemCall(int syscallNumber)
{
	int* call = (int*)(-1);
	*call = syscallNumber;
	return;
}

void print(char* toPrint)
{
	(SystemCall(1));
	return;
}

char* c;

//int fib(int n);

char fib(char n)
{
	//n = 5;
	return n;
}

int main()
{
	c = "hello world";

	char i = 0;
	
	while (i < 10)
	{
		(print(c));
		i++;
	}

	i = 1;

	c[1] = 0;

	while (i < 10)
	{
		c[0] = i + 48;
		(print(c));
		//c[0] = fib(i) + 48;
		//(print(c));
		i = (i + 1);
	}
	
	while (1){}
}