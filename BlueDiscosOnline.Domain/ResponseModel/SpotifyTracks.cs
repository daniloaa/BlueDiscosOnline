using System;
using System.Collections.Generic;
using System.Text;

namespace BlueDiscosOnline.Domain.ResponseModel
{
    public class SpotifyTracks
    {
        public List<SpotifyArtist> Artists { get; set; }
        public SpotifyAlbum Album { get; set; }        
    }
}
