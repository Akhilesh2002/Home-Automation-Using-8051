#include <reg51.h>

sbit Device1 = P0 ^ 0;
sbit Device2 = P0 ^ 1;
sbit Device3 = P0 ^ 2;

char str;
char Charin = 0;

void delay(int time)
{
	unsigned int i, j;
	for (i = 0; i < time; i++)
		for (j = 0; j < 1275; j++);
}

void Serialwrite(char byte)
{
	SBUF = byte;
	while (!TI)
		;
	TI = 0;
}

void Serialprintln(char *p)
{
	while (*p)
	{
		Serialwrite(*p);
		p++;
	}
	Serialwrite(0x0d);
}

void Serialbegin()
{
	TMOD = 0x20;
	SCON = 0x50;
	TH1 = 0xfd;
	TR1 = 1;
}

void main()
{
	P2 = 0x00;
	Serialbegin();
	Serialprintln("System Ready ... ");
	delay(50);
	while (1)
	{
		while (!RI);
		Charin = SBUF;
		str = Charin;
		RI = 0;
		if (str == '1')
		{
			Device1 = 1;
			Serialprintln("Device 1 ON");
			delay(50);
		}
		else if (str == '2')
		{
			Device1 = 0;
			Serialprintln("Device 1 OFF");
			delay(50);
		}
		else if (str == '3')
		{
			Device2 = 1;
			Serialprintln("Device 2 ON");
			delay(50);
		}
		else if (str == '4')
		{
			Device2 = 0;
			Serialprintln("Device 2 OFF");
			delay(50);
		}
		else if (str == '5')
		{
			Device3 = 1;
			Serialprintln("Device 3 ON");
			delay(50);
		}
		else if (str == '6')
		{
			Device3 = 0;
			Serialprintln("Device 3 OFF");
			delay(50);
		}
		str = 0;
	}
}
