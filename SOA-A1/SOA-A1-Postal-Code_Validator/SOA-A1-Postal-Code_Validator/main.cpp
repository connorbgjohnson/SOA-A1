///Project: SOA-A1-Postal-Code_Validator
///File: PostalCode.cpp
///Date: 2018/01/04
///Author: Lauchlin Morrison
///Application runs as a service to be queried for canadian postal code validation.


#include "PostalCode.h"

using namespace std;
using namespace PostalCode;

int main(int, char*[])
{
	bool isProvinceValid = validateProvince("NL");
	bool isValid = validatePostalCode("NL", "asdasd");
	bool miaow = validatePostalCodeFormat("N2L 1C8");
	bool miaow2 = validatePostalCodeFormat("N2L1C8");
	bool miaow3 = validatePostalCodeFormat("N2L YRE");

}