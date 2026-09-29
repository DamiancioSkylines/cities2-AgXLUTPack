@echo off
setlocal enabledelayedexpansion

rem Prepare path to input config and output folder
set "OCIO=%~dp0colormanagement\config.ocio"
set "OutputFolder=%~dp0..\LUTS"
if not exist "%OutputFolder%" mkdir "%OutputFolder%"

echo:
echo OutputFolder: %OutputFolder%
echo:

rem Prepare list of looks to bake
set "LOOKS=Punchy;Greyscale;Very High Contrast;High Contrast;Medium High Contrast;Base Contrast;Medium Low Contrast;Low Contrast;Very Low Contrast"

for %%L in ("%LOOKS:;=" "%") do (

	set "LOOK_SUFFIX=%%~L"
	set "LOOK_NAME=AgX - %%~L"
	
	rem Prepare clean names
	set "SAFE_NAME=!LOOK_SUFFIX: = !"
	set "FILE_NAME=AgX - !SAFE_NAME!"

	echo Baking: !FILE_NAME!.cube

    rem ---------------------------------------------------------------------------
	rem Cities Skylines 2 External LUT pipeline:
	rem scene linear Rec.709 > LinearToLogC from shader LogC EI1000 (0-1) > External LUT > linear Rec.709 > Game encodes to sRGB > Display
	rem Input: "CS2 LogC EI1000" configuration matches Unity LogC EI 1000 in the shader that not uses Precise version but faster one without using last two parameters
	rem Output: "AgX Linear Rec.709" gives the game back scene linear back to the engine, then game does final display transform.
	rem Shortly LUT gets feed simplified LogC and suppose to output Linear Rec.709 out.
	rem There is no shaper needed, the game shader compresses HDR into 0-1 range.
	rem Probably its best to bake 65 resolution to use as base LUT for davinci, but for game 32 safer for performance and unity wiki says its sufficient.
	rem ---------------------------------------------------------------------------

	ociobakelut --iconfig "%OCIO%" ^
				--inputspace "CS2 LogC EI1000" ^
				--outputspace "AgX Linear Rec.709" ^
				--looks "!LOOK_NAME!" ^
				--format resolve_cube "%OutputFolder%\!FILE_NAME!.cube" ^
				--cubesize 32
)

echo:
echo Baking complete.
echo:
PAUSE