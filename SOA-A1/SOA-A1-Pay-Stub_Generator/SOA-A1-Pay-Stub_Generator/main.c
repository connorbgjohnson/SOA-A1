///Project: SOA-A1-Pay-Stub_Generator
///File: main.c
///Date: 2018/01/06
///Author: Lauchlin Morrison
///

#include <stdio.h>
#include "paystub.h"


int main(int argc, char* argv)
{
	char* tagName = "PAYROLL";

	float generatedPay = 0;
	printf("Error code: %d \n", payStubGenerate("HOUR", 36.0f, 15.23, 0, 0, &generatedPay));
	printf("Float value: %2.6f \n\n", generatedPay);

	printf("Error code: %d \n", payStubGenerate("FULL", 40.0f, 56000.00, 0, 0, &generatedPay));
	printf("Float value: %2.6f \n\n", generatedPay);

	printf("Error code: %d \n", payStubGenerate("SEASON", 36.0f, 2.30, 360, 0, &generatedPay));
	printf("Float value: %2.6f \n\n", generatedPay);

	printf("Error code: %d \n", payStubGenerate("CONTRACT", 40.0f, 9699.99, 0, 4, &generatedPay));
	printf("Float value: %2.6f \n\n", generatedPay);

	return 0;
}