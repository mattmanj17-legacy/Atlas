int div(int dividend, int divisor) 
{
	int denom = divisor;
	int current = 1;
	int answer = 0;

	if (denom > dividend)
		return 0;

	if (denom == dividend)
		return 1;

	while (denom <= dividend) {
		denom = denom << 1;
		current = current << 1;
	}

	denom = denom >> 1;
	current = current >> 1;

	while (current != 0) {
		if (dividend >= denom) {
			dividend = dividend - denom;
			answer = answer | current;
		}
		current >>= 1;
		denom >>= 1;
	}
	return answer;
}

int mod(int a, int b)
{
	return (a - div(a, b) * b);
}

// A utility function to reverse a string 
void reverse(char* str, int length)
{
	int start = 0;
	int end = length - 1;
	while (start < end)
	{
		char old = *(str + start);
		*(str + start) = *(str + end);
		*(str + end) = old;
		
		start = start + 1;
		end = end - 1;
	}
	return;
}

// Implementation of itoa()
char* itoa(int num, char* str, int base)
{
	int i = 0;
	int isNegative = 0;

	/* Handle 0 explicitely, otherwise empty string is printed for 0 */
	if (num == 0)
	{
		*(str + i) = '0';
		i = i + 1;
		*(str + i) = '\0';
		return str;
	}

	// In standard itoa(), negative numbers are handled only with 
	// base 10. Otherwise numbers are considered unsigned.
	if (num < 0 && base == 10)
	{
		isNegative = 1;
		num = -num;
	}

	// Process individual digits
	while (num != 0)
	{
		int rem = mod(num, base);
		*(str + i) = (rem > 9) ? (rem - 10) + 'a' : rem + '0';
		num = div(mod / base);
	}

	// If number is negative, append '-'
	if (isNegative){
		*(str + i) = '-';
		i = i + 1;
	}

	*(str + i) = '\0'; // Append string terminator

	// Reverse the string
	reverse(str, i);

	return str;
}

char* buffer = "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0";

void OB(int out)
{
	asm("OB");
	return;
}

int IB()
{
	asm("IB");
	asm("RETV");
}

void cout(char* out)
{
	int index = 0;
	while (*(out + index) != '\0')
	{
		OB(*(out + index));
		index = index + 1;
	}
	return;
}

int main()
{
	(cout("Base:10 ")); (cout(itoa( 1567, buffer, 10))); (cout("\n"));
	(cout("Base:10 ")); (cout(itoa(-1567, buffer, 10))); (cout("\n"));
	(cout("Base:2 " )); (cout(itoa( 1567, buffer, 2 ))); (cout("\n"));
	(cout("Base:8 " )); (cout(itoa( 1567, buffer, 8 ))); (cout("\n"));
	(cout("Base:16 ")); (cout(itoa( 1567, buffer, 16))); (cout("\n"));
	while (1){}
}