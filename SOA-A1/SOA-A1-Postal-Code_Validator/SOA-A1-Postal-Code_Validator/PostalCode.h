#include <string>

#pragma once

using namespace std;

namespace PostalCode
{
	bool validatePostalCode(string province, string code);
	string getSpecialMessage(string province, string code);
	bool validateProvince(string province);
}