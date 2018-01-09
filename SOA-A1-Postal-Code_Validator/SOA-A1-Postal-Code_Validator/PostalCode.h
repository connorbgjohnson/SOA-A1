///Project: SOA-A1-Postal-Code_Validator
///File: PostalCode.cpp
///Date: 2018/01/04
///Author: Lauchlin Morrison
///File contains all the function declarations for validating postal codes.

#pragma once
#include <string>

namespace PostalCode
{
	///This uses the other helper functions to perform all the postal code validation.
	///It returns a bool value stating the success and the additional info is passed using an "out" parameter.
	bool validate(std::string provinceCode, std::string postalCode, std::string &specialNotes);

	///Format input to upper case.
	std::string formatToUpper(std::string province);

	///Format the postal code better for parsing.
	std::string formatRemoveSpace(std::string code);

	///Returns if the province code exists or not.
	bool validateProvince(std::string province);

	///Ensures the postal code is formatted correctly.
	bool validatePostalCodeFormat(std::string code);

	///Validates that the postal code works in the context of the applications requirements.
	bool validatePostalCode(std::string province, std::string code);

	///Pull out a special message if it exists.
	std::string getSpecialMessage(std::string province, std::string code);

	///Check if the error is due to incorrect structure.
	bool checkIfCodeStructureError(std::string code);

	///Check if the error is due to incorrect errors.
	bool checkIfLetterError(std::string code);
}