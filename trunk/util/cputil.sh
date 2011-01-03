#!/bin/bash

#No longer used

p="AccumulatedSummary Overlay PerformancePredictor HighScore TRIMP UniqueRoutes"
#s=PerformancePredictor

s=$1
if [ "$s" == "" -o ! -d ${s}Plugin/Util ]; then
  printf "Cannot find plugin source dir ($s)(${s}Plugin/Util)\n"
  exit 1
fi

for d in $p; do
  if [ "$s" != "$d" ]; then
    echo "$d"
    #cp ${s}Plugin/Util/*.cs ${d}Plugin/Util/
    #cp ${s}Plugin/Util/StringResources.resx ${d}Plugin/Util/
    for f in UnitUtil.cs WarningDialog.cs YesNoDialog.cs StringResources.Designer.cs StringResources.resx; do
    cp ${s}Plugin/Util/$f ${d}Plugin/Util/
    done

#    env s=$s d=$d perl -pi -e 's/SportTracks$ENV{s}Plugin/SportTracks$ENV{d}Plugin/;' ${d}Plugin/Util/*.cs
  fi
done


