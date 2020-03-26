# LyricMaker
It's a nuget package to import/export lyric file and edit time tag.

## How to use : 

Import : 
``` Csharp
vat lyricText = ...
var decoder = new LrcDecoder();
var song = decoder.Decode(lyricText);

//Now you can edit song by editor
```

Editor : 
``` Csharp

//TODO : 

```

Export : 

``` Csharp

var encoder = new LrcEncoder();
var newLyricText = encoder.Encode(song);
//TODO save string into file

```