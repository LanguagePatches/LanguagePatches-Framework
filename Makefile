# Language Patches Makefile
# Copyright 2016 Thomas

# We only support roslyn compiler at the moment
CS := csc
MONO_ASSEMBLIES := /usr/lib/mono/2.0

# Build Outputs
CURRENT_DIR := $(shell pwd)
PLUGIN := LanguagePatches.dll

# Code paths
CODE := $(CURRENT_DIR)/LanguagePatches

# Assembly References
CORLIB := $(MONO_ASSEMBLIES)/mscorlib.dll,$(MONO_ASSEMBLIES)/System.dll,$(MONO_ASSEMBLIES)/System.Core.dll
REFS := $(CORLIB),Assembly-CSharp.dll,UnityEngine.dll,UnityEngine.UI.dll,System.Regex.dll

# Zip File
ZIP_NAME := LanguagePatches-$(shell git describe --tags)-$(shell date "+%Y-%m-%d").zip

### BUILD TARGETS ###
all: plugin
framework: $(PLUGIN)
plugin: framework
	zip $(ZIP_NAME) $(PLUGIN) System.Regex.dll
	
### LIBRARIES ###
$(PLUGIN): 
	$(CS) /debug+ /debug:portable /out:$(PLUGIN) /nostdlib+ /target:library /platform:anycpu /recurse:$(CODE)/*.cs /reference:$(REFS)

### UTILS ###
clean:
	rm *.dll