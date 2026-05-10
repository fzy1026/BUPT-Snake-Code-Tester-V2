一、下载 GCC

1. 打开官网

网址：

https://winlibs.com/

打开后往下滑，找到：

Download

2. 选择下载哪个版本

你是普通 Windows 电脑，基本选这个：

Win64
without LLVM/Clang/LLD/LLDB
Zip archive

也就是说，你优先找类似这一行：

GCC 最新版本 + MinGW-w64 ... (UCRT)
Win64 (without LLVM/Clang/LLD/LLDB): Zip archive



二、解压 GCC

下载完成后，你会得到一个压缩包，名字大概类似：

winlibs-x86_64-posix-seh-gcc-xxx-mingw-w64ucrt-xxx.zip

右键这个压缩包，选择：

全部解压缩

建议解压到 C 盘根目录附近，方便以后找。

比如你可以解压到：

C:\

解压后，你可能会看到一个很长名字的文件夹，比如：

C:\winlibs-x86_64-posix-seh-gcc-16.1.0-mingw-w64ucrt-14.0.0-r1

打开它，里面应该有一个文件夹：

mingw64

继续打开：

mingw64

里面应该能看到：

bin
include
lib
share

再打开 bin，里面应该能看到：

gcc.exe
g++.exe
gdb.exe

只要你能看到 gcc.exe，说明你下载和解压都没问题。



三、整理文件夹路径

为了后面方便，我建议你把 mingw64 文件夹直接放到 C 盘根目录。

最后变成这样：

C:\mingw64

然后里面是：

C:\mingw64\bin
C:\mingw64\include
C:\mingw64\lib

你最终要加入环境变量的路径是：

C:\mingw64\bin

注意，不是：

C:\mingw64

也不是：

C:\mingw64\bin\gcc.exe

正确的是：

C:\mingw64\bin

判断标准只有一句：

哪个 bin 文件夹里有 gcc.exe，就把那个 bin 文件夹的路径加到 Path。



四、添加环境变量 Path

这一步最关键。

方法一：用 Windows 搜索打开

按键盘左下角的 Windows 键，搜索：

环境变量

点击：

编辑系统环境变量

会弹出一个窗口，名字叫：

系统属性

在这个窗口右下角，点击：

环境变量
五、在“用户变量”里添加 Path

打开“环境变量”窗口后，你会看到上下两部分：

用户变量
系统变量

优先改上面的：

用户变量

因为这样只影响你当前账号，比较安全。

在“用户变量”里面找到：

Path

点一下 Path，然后点：

编辑

进入后，点击右侧：

新建

然后粘贴：

C:\mingw64\bin

然后一路点击：

确定
确定
确定

把所有窗口都关掉。



六、重新打开 CMD

注意，这一步不能省。

如果你之前已经打开了 CMD、PowerShell、Windows Terminal，要全部关掉。

然后重新打开一个新的 CMD。

打开方法：

按 Windows 键，搜索：

cmd

打开：

命令提示符

输入：

gcc --version

如果出现 GCC 版本信息，比如：

gcc.exe (MinGW-W64 x86_64-ucrt-posix-seh) ...

说明安装成功。
