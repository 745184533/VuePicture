// pch.cpp: 与预编译标头对应的源文件

#include "pch.h"
#include<cstring>
#include <iostream>
using namespace std;
extern "C" _declspec(dllexport)const char* getFileName(char* path) {
	
	cout << "检测调用输入" << endl;
	cout << path << endl;
	string paths = string(path);
	int pos=paths.find_last_of("/");
	string name = paths.substr(pos);

	int len = name.length();
	//char * ch = new char[len + 1];	//分配存储空间
	//char ch[40] = "";
	try {
		cout << name << endl;
		const char* ch = name.data();
		//strcpy_s(ch, len + 1, name.c_str());
		cout << ch << endl;
		cout << "success" << endl;
		return ch;
	}
	catch (exception & e) {
		cerr << e.what() << endl;
	}
	//strcpy_s(ch, "sucess");
	
}

//extern "C" _declspec(dllexport)int getFileID(char* path) {
//
//	cout << "检测调用输入" << endl;
//	cout << path << endl;
//	string paths = string(path);
//	int pos = paths.find_last_of("/");
//	string name = paths.substr(pos);
//
//	int len = name.length();
//	//char * ch = new char[len + 1];	//分配存储空间
//	//char ch[40] = "";
//	try {
//		cout << name << endl;
//		const char* ch = name.data();
//		//strcpy_s(ch, len + 1, name.c_str());
//		cout << ch << endl;
//		cout << "success" << endl;
//		return 0;
//	}
//	catch (exception& e) {
//		cerr << e.what() << endl;
//	}
//	//strcpy_s(ch, "sucess");
//
//}
// 当使用预编译的头时，需要使用此源文件，编译才能成功。
