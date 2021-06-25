#include "pch.h"

#include "clock.h"

namespace clockMe {

	
	System::Int32 Calclock::startClock()
	{
		return clock();//程序开始计时
	}
	System::Int32 Calclock::endClock()
	{
		return clock();//程序结束计时
	}
}

