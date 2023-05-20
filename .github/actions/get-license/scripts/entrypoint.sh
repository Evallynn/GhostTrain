#!/usr/bin/env bash

if [ -n "$INPUT_WORKDIR" ] ; then
  cd "$INPUT_WORKDIR"
fi

FILE_PATH=UnityLicenseFile.ulf

echo "$UNITY_LICENSE" | tr -d '\r' > $FILE_PATH

pwd

ls -l

xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' \
/opt/Unity/Editor/Unity \
  -batchmode \
  -nographics \
  -logfile /dev/stdout \
  -silent-crashes \
  $INPUT_ARGS