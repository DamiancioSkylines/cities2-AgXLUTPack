# AgX LUT Baking

This folder contains the OCIO configuration adapted from Blender 5.2 version and script to generate LUT files for the pack.

### Cities Skylines 2 External LUT pipeline:

Shortly External LUT's in game gets feed simplified/fast LogC EI1000 (0-1 range) and suppose to output Linear Rec.709 out.

**Transform Flow:**
scene linear Rec.709 > LinearToLogC from shader LogC EI1000 (0-1) > External LUT > linear Rec.709 > Game finally encodes to sRGB on its own > Display

To make it work config.ocio I included two new ColorSpace encodings to handle baking AgX LUT to what we need.

* **Input:** "CS2 LogC EI1000" configuration matches Unity LogC EI 1000 in the shader that not uses Precise version but faster one without using last two parameters because USE_PRECISE_LOGC is set to 0 by default.
* **Output:** "AgX Linear Rec.709" gives the game back scene linear back to the engine, that does final sRGB display transform.
* **Shaper:** None required, game shader compressed HDR values into 0-1 range before hitting the LUT.
* **Resolution:** 32x32x32 should be optimal for runtime performance.

### How to Run:
1. Ensure **OpenColorIO** is installed and added environment `PATH` so it's accessible via Command Prompt.
2. Run Bake_AgXLUTPack.bat
3. LUT files `.cube` will be baked directly into `../LUTS/`  folder in the solution, with automatic file override.

I have installed OpenColorIO via python with version 2.5.0

```
pip install opencolorio==2.5.0
```

Or to install the latest:

```
pip install opencolorio
```
https://opencolorio.readthedocs.io/en/latest/quick_start/installation.html

### Credits & Licensing
- OpenColorIO: Distributed under the BSD 3-Clause Licence. Copyright (c) 2003-Present Contributors to the OpenColorIO Project.
- AgX Color Transform: Originally researched and created by Troy Sobotka.
- Blender Configuration: Based on core configurations provided by the Blender Foundation.
- Project Licence: This modified configuration and its scripts are provided under the BSD 3-Clause Licence (see the accompanying LICENSE file for details).