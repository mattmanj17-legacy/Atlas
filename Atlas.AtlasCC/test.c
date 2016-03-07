//SYSTEMCALLS
//to make a system call, call a function which has the correct parameters
//then call SystemCall. writing to address -1 causes a system call
//system call table

void SystemCall(int syscallNumber)
{
	int* call = (int*)(-1);
	*call = syscallNumber;
	return;
}

// 1 : print(char* c) --- prints a null terminated string to the console, and a newline;
void print(char* toPrint)
{
	(SystemCall(1));
	return;
}

// 2 : printInt(int c) --- prints a number to the console and a newline;
void printInt(int toPrint)
{
	(SystemCall(2));
	return;
}

//-------------------------------------------

int fib(int n);

int fib(int n)
{
	if (n < 3) return 1;
	else return fib(n - 1) + fib(n - 2);
}

int main()
{
	(print("start main"));

	int i = 1;
	int fibi = 0;

	while (i < 1000)
	{
		fibi = fib(i);
		(printInt(fibi));
		i++;
	}

	(print("done"));

	while (1){}
}