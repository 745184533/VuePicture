// Simple.cpp: CSimple 的实现

#include "pch.h"
#include "Simple.h"


// CSimple



STDMETHODIMP CSimple::testRule(LONG blogID, LONG* Rule)
{
    // TODO: 在此处添加实现代码
    *Rule = blogID + 1;

    return S_OK;
}
