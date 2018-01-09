///Project: SOA-A1-Pay-Stub_Generator
///File: paystub.c
///Date: 2018/01/06
///Author: Lauchlin Morrison
///Definitions for the functions that handle generating paystubs from user information.

#pragma once
#include "paystub.h"
#include <string.h>
#include <math.h>


//
// Name: payStubGenerate(const char*, float, float, float, int, float*)
// Parameters: 
// const char* employeeType: The type of employee
// float hoursWorked
// int payRate
// int pieces
// int weeksWorked
// float* generatePay: The pay to generate
// Returns: An int with the payStubGenerate
// Description:
// Generate the paystub value.
//
int payStubGenerate(const char* employeeType, float hoursWorked, float payRate, int pieces, int weeksWorked, float* generatedPay)
{
	int error = 0;
	int employeeCode = validateEmployeeType(employeeType);
	if (employeeCode >= 0)
	{
		if (employeeCode == EMPLOYEE_CODE_HOURLY)
		{
			*generatedPay = calculateHourly(payRate, hoursWorked);
		}
		else if (employeeCode == EMPLOYEE_CODE_FULLTIME)
		{
			*generatedPay = calculateFulltime(payRate);
		}
		else if (employeeCode == EMPLOYEE_CODE_SEASONAL)
		{
			*generatedPay = calculateSeasonal(payRate, hoursWorked, pieces);
		}
		else if (employeeCode == EMPLOYEE_CODE_CONTRACT)
		{
			*generatedPay = calculateContract(payRate, weeksWorked);
		}
	}
	else
	{
		//Invalid employee type.
		error = ERROR_INVALID_EMPLOYEE_TYPE;
	}

	return error;
}

//
// Name: validateEmployeeType(const char*)
// Parameters: 
// const char* employeeType: The type of employee
// 
// Returns: An int to tell you whether the the employee is valid
// Description:
// Validates the employee type
//
int validateEmployeeType(const char* employeeType)
{
	int type = -1;

	if (strcmp(employeeType, EMPLOYEE_VALUE_HOURLY) == 0)
	{
		type = EMPLOYEE_CODE_HOURLY;
	}
	else if (strcmp(employeeType, EMPLOYEE_VALUE_FULLTIME) == 0)
	{
		type = EMPLOYEE_CODE_FULLTIME;
	}
	else if (strcmp(employeeType, EMPLOYEE_VALUE_SEASONAL) == 0)
	{
		type = EMPLOYEE_CODE_SEASONAL;
	}
	else if (strcmp(employeeType, EMPLOYEE_VALUE_CONTRACT) == 0)
	{
		type = EMPLOYEE_CODE_CONTRACT;
	}

	return type;
}
//
// Name: calculateHourlys(const char*)
// Parameters: 
// float rate: The rate per hour
// float hours: The amount of hours.
// Returns: An int to tell you whether the the employee is valid
// Description:
// Validates the employee type
//
float calculateHourly(float rate, float hours)
{
	float pay = 0.0f;
	if (hours <= HOURS_IN_WEEK)
	{
		pay = hours * rate;
	}
	else if (hours > HOURS_IN_WEEK)
	{
		pay = (hours * rate) + ((hours - HOURS_IN_WEEK) * (rate * 1.5f));
	}
	return pay;
}
//
// Name: calculateFulltime(float salary)
// Parameters: 
// float salary: The salary amount.
// Returns: The salary
// Description:
// Calculates the full time value.
//
float calculateFulltime(float salary)
{
	return salary / 52;
}
//
// Name: calculateSeasonal(float, float, int)
// Parameters: 
// float piecePay: The piece payd
// Returns: The salary
// Description:
// Calculates the full time value.
//
float calculateSeasonal(float piecePay, float hours, int pieces)
{
	float pay = pieces * piecePay;
	if (hours >= HOURS_IN_WEEK)
	{
		pay += 150.00f;
	}
	return pay;
}
// Name: calculateContract(float, float, int)
// Parameters: 
// float contractAmount: The contract amount
// int weeks: The weeks themselves.
// Returns: The salary
// Description:
// Calculates the contract amount 
//
float calculateContract(float contractAmount, int weeks)
{
	return contractAmount / weeks;
}