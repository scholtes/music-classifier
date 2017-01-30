Included data
=============

The data is split between 3 directories.  

 * `Raw` - Contains data, per listener, as each listener recorded it during data collection.  It also contains raw `bextract` information`  
 * `Formatted` - Contains data, per listener, reformatted to be in the same format  
   - `id` - Song id  
   - `positivity` - Also known as "valence". How positive a song is emotionally.  Higher is more positive, lower is more negative  
   - `intensity` - Also known as "energy" or "arousal".  How intense a song is.  Higher is more intense, lower is more calm or mellow  
   - `confidence` - Each listener rated how confident they were with their positivity and intensity ratings.  Higher is more confident for a song, lower is less confident   
   - 'bextract1'-'bextract62' - Audio metrics extracted from each song segment, as given by the `bextract` utility in the Marsyas project  
 * `Normalized` - Data combined from all 4 listeners.  Each listener used a different scale for each attribute.  The data was fixed to run from -1 to 1 (adjusted for outliers)  
   - `all_norms.xlsx` - Data per listener  
   - `median_data.xlsx` - The median of each value across all the listeners for each song  
