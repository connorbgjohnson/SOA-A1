///Project: SOA-A1-Postal-Code_Validator
///File: PostalCode.cpp
///Date: 2018/01/04
///Author: Lauchlin Morrison
///File contains all the function declarations for validating postal codes.

#pragma once
#include <string>

using namespace std;

namespace PostalCode
{
	///This uses the other helper functions to perform all the postal code validation.
	///It returns a bool value stating the success and the additional info is passed using an "out" parameter.
	bool validate(string provinceCode, string postalCode, string &specialNotes);

	///Format input to upper case.
	string formatToUpper(string province);

	///Format the postal code better for parsing.
	string formatRemoveSpace(string code);

	///Returns if the province code exists or not.
	bool validateProvince(string province);

	///Ensures the postal code is formatted correctly.
	bool validatePostalCodeFormat(string code);

	///Validates that the postal code works in the context of the applications requirements.
	bool validatePostalCode(string province, string code);

	///Pull out a special message if it exists.
	string getSpecialMessage(string province, string code);

	///Check if the error is due to incorrect structure.
	bool checkIfCodeStructureError(string code);

	///Check if the error is due to incorrect errors.
	bool checkIfLetterError(string code);
}