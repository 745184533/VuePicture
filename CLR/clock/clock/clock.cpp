#include "pch.h"

#include "clock.h"

namespace clockMe {

	
	System::Int32 Calclock::startClock()
	{
		return clock();//����ʼ��ʱ
	}
	System::Int32 Calclock::endClock()
	{
		return clock();//���������ʱ
	}
}

