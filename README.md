# SR-8-Channel export/import tool

## Preface

Some years ago, I bought an Alinco DX-SR8E. The rig does its job very well and also has an affordable price. 
However, I got never used to the channel programming on the device itself. Fortunately, there is an option to edit the channels via PC and upload the data to the radio.

You can either use an orignal Alinco ERW-7 cable or build on on your own. [1] Then fire up the Alinco DX-SR8 Clone Utility tool, edit your channels and upload the data back to the radio.

But I found that editing the channels with DX-SR8 Clone Utility is just a pain: You cannot edit the table with the memory entries, you have to open a dialogue box for every channel, then edit MHz, KHz and Hz seperately. You cannot import channel list, e.g. downloaded from a repeater or DXPedition list etc. While the site states "You may also use import/export function so that you can edit the memory data using common CSV format.", I could not find that option.

So I decided to write an import/export tool for the SR8 files DX-SR8 Clone Utility creates. I found out that these files simply consist of 607 lines of hexadecimal strings. I already assumed that every line contains information for one channel, and I was right. By changing the settings of channels step for step and comparing the file before and after, I was able to reengineer the structure of the SR8 files.

My plan then was to build my own channel editor, but then I thought that an export-/import to CSV would be the first logical step towards this. So you can export the channel settings via KD-SDR, adapt the resulting CSV via your favority Spredsheet software or text editor, import the data back to DX-SR8 Clone Utility and upload it to your radio.

Please not that the tool is in a very early stage, but as it may be useful to other YLs and OMs, I decided to upload it. The tool will not perform extensive plausability checks, so use with caution.

## Preparing the CSV file

To create your own template for editing, start DX-SR8 Clone Utility, download your settings from the radio and save the file via File/Save, e.g. C:\ham\myChannels.sr8.

Extract SR8Channels to your computer and export the channel list with

```
C:\ham\SR8Channels export c:\ham\myChannels.sr8 c:\ham\myChannels.csv
```

Then edit the CSV file and save it back, e.g. to C:\ham\myChannels_new.csv

## Re-Import to SR8

Then re-import the file via 

```
C:\ham\SR8Channels import c:\ham\myChannels.sr8 c:\ham\myChannels_new.csv c:\ham\myChannels_new.sr8
```

(please note that you must specify the original SR8 file, too. This is due to the fact that the file header contains the general settings for the radio which are not changed by the tool.)

You can then go back to the DX-SR8 Clone Utility and open the new SR8 file as usual and upload it back to your radio.

## Data format for CSV entries

For details on the settings inside the CSV file, please refer to the SR8 file documentation, included in the SR8file_dok.rtf. 
Channels not defined in the CSV file will not be overriden in the SR8 file but keep them as they were.

## What the tool does not

At the moment, the tool does not
* Check frequency values etc.
* Check for double channel definitions - the last one in the file "wins". 
* Read or write files from or to your radio - use the official Alinco tool



[1] http://www.dl9bu.de/cms/2016/10/09/datenkabel-fuer-dj-500e/

[2] http://www.alinco.com/Products/ham/hf/DX-R8/utildown.html
