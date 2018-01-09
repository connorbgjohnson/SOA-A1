///Project: SOA-A1-Postal-Code_Validator
///File: split.h
///Date: 2018/01/04
///Author: Lauchlin Morrison
///Methods for splitting strings.

#pragma once

#include <string>
#include <sstream>
#include <vector>
#include <iterator>

template<typename Out>
void split(const std::string &s, char delim, Out result);
std::vector<std::string> split(const std::string &s, char delim);