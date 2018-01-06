///Project: SOA-A1-Postal-Code_Validator
///File: PostalCode.cpp
///Date: 2018/01/04
///Author: Lauchlin Morrison
///File contains all the function declarations for validating postal codes.

#include <string>
#pragma once

using namespace std;

namespace PostalCode
{
	bool validate(string provinceCode, string postalCode, string &specialNotes);
	string formatToUpper(string province);
	string formatRemoveSpace(string code);
	bool validateProvince(string province);
	bool validatePostalCodeFormat(string code);
	bool validatePostalCode(string province, string code);
	string getSpecialMessage(string province, string code);
	bool checkIfCodeStructureError(string code);
	bool checkIfLetterError(string code);
}