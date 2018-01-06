///Project: SOA-A1-Postal-Code_Validator
///File: PostalCode.cpp
///Date: 2018/01/04
///Author: Lauchlin Morrison
///File containing all the function definitions for validating postal codes.

#include "PostalCode.h"
#include <regex>

namespace PostalCode
{
	///This uses the other helper functions to perform all the postal code validation.
	///It returns a bool value stating the success and the additional info is passed using an "out" parameter.
	bool validate(string provinceCode, string postalCode, string &specialNotes)
	{
		bool isPostalCodeValid = false;

		//Format forgivable mistakes.
		postalCode = formatRemoveSpace(postalCode);
		provinceCode = formatToUpper(provinceCode);

		if (validateProvince(provinceCode))	//Is the province code correct?
		{
			if (validatePostalCodeFormat(postalCode))//Is the postal code formatted correctly?
			{
				if (validatePostalCode(provinceCode, postalCode))	//Is the postal code correct according to 
				{
					isPostalCodeValid = true;
					specialNotes = getSpecialMessage(provinceCode, postalCode);
				}
				else
				{
					specialNotes = "This postal code is not valid.";
				}
			}
			else
			{
				//Postal code is not formatted correctly.
				if (checkIfCodeStructureError(postalCode))
				{
					specialNotes = "The postal code is not structured properly";
				}

				if (checkIfLetterError(postalCode))
				{
					if (specialNotes != "")
					{
						specialNotes += "and the it is using one or more illegal letters!";
					}
					else
					{
						specialNotes = "The postal code is using one or more illegal letters!";
					}
				}
			}
		}
		else
		{
			//Province code is incorrect.
			specialNotes = "The province code is not correct!";
		}

		return isPostalCodeValid;
	}

	///Format input to upper case.
	string formatToUpper(string province)
	{
		string upperProvince = "";

		for (int i = 0; i < province.length(); i++)
		{
			upperProvince += toupper(province[i]);
		}

		return upperProvince;
	}

	///Format the postal code better for parsing.
	string formatRemoveSpace(string code)
	{
		if (code[3] == ' ' && code.length() > 5)
		{
			code = code.substr(0, 3) + code.substr(4, 3);
		}

		return code;
	}

	///Ensures the postal code is formatted correctly.
	bool validatePostalCodeFormat(string code)
	{
		bool isValid = false;

		regex postalCode_regex("[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]");
		if (regex_match(code, postalCode_regex))
		{
			isValid = true;
		}

		return isValid;
	}

	///Validates that the postal code works in the context of the applications requirements.
	bool validatePostalCode(string province, string code)
	{
		bool isValid = false;

		//Newfound land
		if (province == "NL")
		{
			string c = code.substr(0, 2);
			if (c == "A0" ||
				c == "A1" ||
				c == "A2" ||
				c == "A5" ||
				c == "A8")
			{
				isValid = true;
			}
		}

		//Nova Scotia
		if (province == "NS")
		{
			string c = code.substr(0, 2);
			if (c == "B0" ||
				c == "B1" ||
				c == "B2" ||
				c == "B3" ||
				c == "B4" ||
				c == "B5" ||
				c == "B6" ||
				c == "B9")
			{
				isValid = true;
			}
		}

		//New Brunswick
		if (province == "NB")
		{
			string c = code.substr(0, 2);
			if (c == "E1" ||
				c == "E2" ||
				c == "E3" ||
				c == "E4" ||
				c == "E5" ||
				c == "E6" ||
				c == "E7" ||
				c == "E8" ||
				c == "E9")
			{
				isValid = true;
			}
		}

		//Prince Edwards Island
		if (province == "PE")
		{
			string c = code.substr(0, 2);
			if (c == "C0" ||
				c == "C1")
			{
				isValid = true;
			}
		}

		//Quebec
		if (province == "QC")
		{
			string c = code.substr(0, 2);
			if (c == "G0" ||
				c == "G1" ||
				c == "G2" ||
				c == "G3" ||
				c == "G4" ||
				c == "G5" ||
				c == "G6" ||
				c == "G7" ||
				c == "G8" ||
				c == "G9" ||
				c == "H0" || 
				c == "H1" || 
				c == "H2" || 
				c == "H3" || 
				c == "H4" || 
				c == "H5" || 
				c == "H7" || 
				c == "H8" || 
				c == "H9" ||
				c == "J0" || 
				c == "J1" || 
				c == "J2" || 
				c == "J3" || 
				c == "J4" || 
				c == "J5" || 
				c == "J6" || 
				c == "J7" || 
				c == "J8" || 
				c == "J9")
			{
				isValid = true;
			}
		}

		//Ontario
		if (province == "ON")
		{
			string c = code.substr(0, 2);
			if (c == "K0" ||
				c == "K1" ||
				c == "K2" ||
				c == "K4" ||
				c == "K6" ||
				c == "K7" ||
				c == "K8" ||
				c == "K9" ||
				c == "L0" ||
				c == "L1" ||
				c == "L2" ||
				c == "L3" ||
				c == "L4" ||
				c == "L5" ||
				c == "L6" ||
				c == "L7" ||
				c == "L8" ||
				c == "L9" ||
				c == "M1" || 
				c == "M2" || 
				c == "M3" || 
				c == "M5" || 
				c == "M6" || 
				c == "M7" || 
				c == "M8" || 
				c == "N0" || 
				c == "N1" || 
				c == "N2" || 
				c == "N3" || 
				c == "N4" || 
				c == "N5" || 
				c == "N6" || 
				c == "N7" || 
				c == "N8" || 
				c == "N9" || 
				c == "P0" ||
				c == "P1" || 
				c == "P2" || 
				c == "P3" || 
				c == "P4" || 
				c == "P5" || 
				c == "P6" || 
				c == "P7" || 
				c == "P8" || 
				c == "P9")
			{
				isValid = true;
			}
		}

		//Manitoba
		if (province == "MB")
		{
			string c = code.substr(0, 2);
			if (c == "R0" ||
				c == "R1" ||
				c == "R2" || 
				c == "R3" || 
				c == "R4" || 
				c == "R5" || 
				c == "R6" || 
				c == "R7" || 
				c == "R8" || 
				c == "R9")
			{
				isValid = true;
			}
		}

		//Saskatchewan
		if (province == "SK")
		{
			string c = code.substr(0, 2);
			if (c == "S0" ||
				c == "S2" ||
				c == "S3" || 
				c == "S4" || 
				c == "S6" || 
				c == "S7" || 
				c == "S9")
			{
				isValid = true;
			}
		}

		//Alberta
		if (province == "AB")
		{
			string c = code.substr(0, 2);
			if (c == "T0" ||
				c == "T1" || 
				c == "T2" || 
				c == "T3" || 
				c == "T4" || 
				c == "T5" || 
				c == "T6" || 
				c == "T7" || 
				c == "T8" || 
				c == "T9")
			{
				isValid = true;
			}
		}

		//British-Columbia
		if (province == "BC")
		{
			string c = code.substr(0, 2);
			if (c == "V0" ||
				c == "V1" ||
				c == "V2" ||
				c == "V3" ||
				c == "V4" ||
				c == "V5" ||
				c == "V6" ||
				c == "V7" ||
				c == "V8" ||
				c == "V9")
			{
				isValid = true;
			}
		}

		//Yukon
		if (province == "YT")
		{
			string c = code.substr(0, 3);
			if (c == "Y0A" ||
				c == "Y0B" || 
				c == "Y1A")
			{
				isValid = true;
			}
		}

		//Northwest Territories
		if (province == "NT")
		{
			string c = code.substr(0, 3);
			if (c == "X0E" ||
				c == "X0F" ||
				c == "X1A")
			{
				isValid = true;
			}
		}

		//Nunavut
		if (province == "NU")
		{
			string c = code.substr(0, 3);
			if (c == "X0A" ||
				c == "X0B" ||
				c == "X0C")
			{
				isValid = true;
			}
		}

		return isValid;
	}

	///Pull out a special message if it exists.
	string getSpecialMessage(string province, string code)
	{
		string msg = "";
		
		//Newfoundland
		if (province == "NL")
		{
			string c = code.substr(0, 2);
			if (c == "A0")
			{
				msg = "Rural Newfoundland";
			}

			if (c == "A3" ||
				c == "A4" ||
				c == "A6" ||
				c == "A7" ||
				c == "A9")
			{
				msg = "Wanna Be Newfoundland";
			}
		}

		//Nova Scotia
		if (province == "NS")
		{
			string c = code.substr(0, 2);
			if (c == "B0")
			{
				msg = "Rural Noa Scotia";
			}

			if (c == "B7" || c == "B8")
			{
				msg = "Wanna Be Nova Scotia";
			}
		}
		
		//New Brunswick
		if (province == "NB")
		{
			string c = code.substr(0, 2);
			if (c == "E0")
			{
				msg = "Wanna be Rural New Brunswick";
			}
		}

		//New Brunswick
		if (province == "PE")
		{
			string c = code.substr(0, 2);
			if (c == "C0")
			{
				msg = "Rural PEI";
			}

			if (c == "C2" || 
				c == "C3" || 
				c == "C4" || 
				c == "C5" || 
				c == "C6" || 
				c == "C7" ||
				c == "C8" || 
				c == "C9")
			{
				msg = "Wanna be PEI";
			}
		}

		//Quebec
		if (province == "QC")
		{
			string c = code.substr(0, 1);
			if (c == "G")
			{
				msg = "Eastern Quebec";
			}

			if (c == "H")
			{
				msg = "Montreal Area";
			}

			if (c == "J")
			{
				msg = "Western / Northern Quebec";
			}

			c = code.substr(0, 2);
			if (c == "H6")
			{
				msg = "Wanna be Quebec";
			}

			if (c == "G0" ||
				c == "H0" ||
				c == "J0")
			{
				msg = "Rural Quebec";
			}

			c = code.substr(0, 3);
			if (c == "G1A")
			{
				msg = "Provincial Government";
			}

			c = code.substr(0, 6);
			if (c == "H0H0H0")
			{
				msg = "Santa Claus";
			}
		}

		//Ontario
		if (province == "ON")
		{
			string c = code.substr(0, 1);
			if (c == "K")
			{
				msg = "Eastern Ontario";
			}

			if (c == "L")
			{
				msg = "Central Ontario";
			}

			if (c == "M")
			{
				msg = "Toronto Area";
			}

			if (c == "N")
			{
				msg = "Western Ontario";
			}

			if (c == "P")
			{
				msg = "Northern Ontario";
			}

			c = code.substr(0, 2);
			if (c == "K0" ||
				c == "L0" ||
				c == "N0" ||
				c == "P0")
			{
				msg = "Rural Ontario";
			}

			if (c == "K3" ||
				c == "K5" ||
				c == "M0" ||
				c == "M4" ||
				c == "M6")
			{
				msg = "Wanna be Ontario";
			}

			c = code.substr(0, 3);
			if (c == "K1A")
			{
				msg = "Government of Canada";
			}

			if (c == "M7A")
			{
				msg = "Queen's Park";
			}
		}

		//Manitoba
		if (province == "MB")
		{
			string c = code.substr(0, 2);
			if (c == "R0")
			{
				msg = "Rural Manitoba";
			}

			if (c == "R2" ||
				c == "R3")
			{
				msg = "Winnipeg Area";
			}
		}

		//Saskatchewan
		if (province == "SK")
		{
			string c = code.substr(0, 2);
			if (c == "S0")
			{
				msg = "Rural Saskatchewan";
			}

			if (c == "S7")
			{
				msg = "Saskatoon Area";
			}

			if (c == "S1" ||
				c == "S5" ||
				c == "S8")
			{
				msg = "Wanna be Saskatchewan";
			}
		}

		//Alberta
		if (province == "AB")
		{
			string c = code.substr(0, 2);
			if (c == "T0")
			{
				msg = "Rural Alberta";
			}

			if (c == "T5" ||
				c == "T6")
			{
				msg = "Edmonton Area";
			}

			if (c == "T2" ||
				c == "T3")
			{
				msg = "Calgary Area";
			}
		}

		//British Columbia
		if (province == "BC")
		{
			string c = code.substr(0, 2);
			if (c == "V0")
			{
				msg = "Rural British Columbia";
			}
		}

		//Yukon
		if (province == "YT")
		{
			string c = code.substr(0, 2);
			if (c == "Y0")
			{
				msg = "Remote NWT Areas";
			}

			if (c == "Y2" ||
				c == "Y3" ||
				c == "Y4" ||
				c == "Y5" ||
				c == "Y6" ||
				c == "Y7" ||
				c == "Y8" ||
				c == "Y9")
			{
				msg = "Wanna be Yukon";
			}

			c = code.substr(0, 3);
			if (c == "Y1A")
			{
				msg = "Whitehorse";
			}
		}

		//Northwest Territories
		if (province == "NT")
		{
			string c = code.substr(0, 2);
			if (c == "X0")
			{
				msg = "Remote NWT Areas";
			}

			if (c == "X2" ||
				c == "X3" ||
				c == "X4" ||
				c == "X5" ||
				c == "X6" ||
				c == "X7" ||
				c == "X8" ||
				c == "X9")
			{
				msg = "Wanna be Yukon";
			}

			c = code.substr(0, 3);
			if (c == "X1A")
			{
				msg = "Yellowknife";
			}
		}

		if (province == "NU")
		{
			msg = "Everything is remote in Nunavut";
		}

		return msg;
	}

	///Returns if the province code exists or not.
	bool validateProvince(string province)
	{
		bool foundProvince = false;

		if( province == "NL" ||
			province == "NS" ||
			province == "NB" ||
			province == "PE" ||
			province == "QC" ||
			province == "ON" ||
			province == "MB" ||
			province == "SK" ||
			province == "AB" ||
			province == "BC" ||
			province == "YT" ||
			province == "NT" ||
			province == "NU")
		{
			foundProvince = true;
		}

		return foundProvince;
	}

	///Check if the error is due to incorrect errors.
	bool checkIfLetterError(string code)
	{
		bool isValid = true;

		regex r("/[DFIOQU]/g");
		if (regex_match(code, r))
		{
			isValid = false;
		}

		return isValid;
	}

	///Check if the error is due to incorrect structure.
	bool checkIfCodeStructureError(string code)
	{
		bool isValid = true;

		regex r("[A-Z][1-9][A-Z][1-9][A-Z][1-9]");
		if (!regex_match(code, r))
		{
			isValid = false;
		}

		return isValid;
	}

}
