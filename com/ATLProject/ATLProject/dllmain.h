// dllmain.h: 模块类的声明。

class CATLProjectModule : public ATL::CAtlDllModuleT< CATLProjectModule >
{
public :
	DECLARE_LIBID(LIBID_ATLProjectLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_ATLPROJECT, "{bfd98fce-fb12-42c0-964a-55f15bffa4f3}")
};

extern class CATLProjectModule _AtlModule;
