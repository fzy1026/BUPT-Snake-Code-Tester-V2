一、下载安装 MSYS2

去 MSYS2 官网下载并安装：
搜索：MSYS2 download

安装时路径建议保持默认：

C:\msys64
二、打开 MSYS2 UCRT64

安装完成后，开始菜单里找到并打开：

MSYS2 UCRT64

注意：不要打开普通的 MSYS2 MSYS，要打开 UCRT64。

三、安装 GCC

在 MSYS2 UCRT64 终端里输入：

pacman -S mingw-w64-ucrt-x86_64-gcc

中途问你是否继续，直接按：

Enter

或者输入：

Y

这个包就是 MinGW-w64 的 GCC 编译器，包含 C、C++、OpenMP 等。

四、配置环境变量 PATH

安装好后，把这个路径加入 Windows 的用户 PATH：

C:\msys64\ucrt64\bin

操作步骤：

Windows 搜索：环境变量
打开：编辑系统环境变量
点：环境变量
在“用户变量”里找到 Path
点：编辑
点：新建
粘贴：
C:\msys64\ucrt64\bin
一路确定
重新打开 CMD / PowerShell / VS Code
五、检查是否安装成功

重新打开终端，输入：

gcc --version

如果出现版本号，比如：

gcc.exe ...

就说明成功了。