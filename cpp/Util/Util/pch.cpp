// pch.cpp: 与预编译标头对应的源文件

#include "pch.h"
#include<string>
using namespace std;
extern "C" _declspec(dllexport)char* getFileName(char* path) {
	string paths = string(path);
	int pos=paths.find_last_of("/");
	string name = paths.substr(pos + 1);
	char ch[40];
	strcpy_s(ch,name.c_str());
	return ch;
}
// 当使用预编译的头时，需要使用此源文件，编译才能成功。
