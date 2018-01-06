///Project: SOA-A1-Postal-Code_Validator
///File: PostalCode.cpp
///Date: 2018/01/04
///Author: Lauchlin Morrison
///Application runs as a service to be queried for canadian postal code validation.

#include "PostalCode.h"
#include <stdio.h>

using namespace std;
using namespace PostalCode;

int main(int, char*[])
{
	//Do connection stuff here.//

	string postalCode = "H0H0H0";
	string provinceCode = "QC";
	string specialNotes = "";
	bool result = validate(provinceCode, postalCode, specialNotes);

	printf(specialNotes.c_str());

}