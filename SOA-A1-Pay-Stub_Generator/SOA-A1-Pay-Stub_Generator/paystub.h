///Project: SOA-A1-Pay-Stub_Generator
///File: paystub.h
///Date: 2018/01/06
///Author: Lauchlin Morrison
///File declares the paystub functions.

#pragma once

///Performs all the logic for generating the pay stub.
///Returns an error code from the function. The pay stub value is returned through the generatedPay pointer passed in.
int payStubGenerate(const char* employeeType, float hoursWorked, float payRate, int pieces, int weeksWorked, float* generatedPay);

///Validate and return the employee code if valid. Otherwise return a negative int.
int validateEmployeeType(const char* employeeType);

///Calculate hourly employee weekly pay.
float calculateHourly(float rate, float hours);

///Calculate fulltime employee weekly pay.
float calculateFulltime(float salary);

///Calculate seasonal employee weekly pay.
float calculateSeasonal(float piecePay, float hours, int pieces);

///Calculate contract employee weekly pay.
float calculateContract(float contractAmount, int weeks);
