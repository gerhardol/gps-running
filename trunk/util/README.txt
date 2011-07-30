See the Developer wiki for more information

Upload to Google Code:

#!/bin/bash

s="Updated German, Italian, French and Spanish translations"
d="2.0.322 2011-07-30
 * All: Updated German, Italian, French and Spanish translations
 * AS: Visual updates
 * HS, PP: Minor fixes"

for f in AccumulatedSummaryPlugin HighScorePlugin OverlayPlugin PerformancePredictorPlugin UniqueRoutesPlugin; do
  util/googlecode_upload.py -p gps-running -u gerhard.nospam -l Featured -s "$s" `ls -rt $f/*plugin| tail -1`
done