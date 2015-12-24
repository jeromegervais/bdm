#!/bin/bash
git checkout -- src/AssemblyInfo.Version.cs
diff=$(git diff --name-status --cached)
tools/lprun/lprun.exe tools/hooks/pre-commit.linq $diff
exit $?