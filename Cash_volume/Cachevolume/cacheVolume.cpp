#include<iostream>
#include<ctime>
#include <stdio.h>
#include <time.h>

using namespace std;


int main()
{
	int n = 10000;
	int* arr = new int[n];
	for (int i = 0; i < n; i++)
	{
		arr[i] = 3;
	}

	unsigned long start = 0,
		end = 0,
		tmp = 0;

	double time = 0,// для измерения времени
		time2 = 0;// 

	int a = 0; // переменная в которую записывается каждый элемент массива

	double LengthCash = 0;// объем кэша 

	for (int k = 0; k < 100; k++)
	{
		for (int i = 0; i < n; i++)
		{
			start = clock();

			a = arr[i];
			a += 3;
			arr[i] = a;

			end = clock();

			time = (double)(end - start) / CLOCKS_PER_SEC;
			if (time2 < time)
			{
				tmp++;
			}
			time2 = time;
		}
		LengthCash += (n / tmp) * 32;
	}
	LengthCash /= 100;


	cout << LengthCash << " bit" << endl;
	cout << LengthCash / 8 << " byte " << endl;
	cout << LengthCash / (1024 * 8) << " kb" << endl;


	system("pause");
	return 0;
}