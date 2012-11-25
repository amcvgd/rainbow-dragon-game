using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace RainbowDragon.HelperClasses
{
    class MusicPlayer
    {
        Dictionary<string, SoundEffectInstance> sounds;
        Dictionary<string, Song> songs;

        Game1 game;
        public MusicPlayer(Game1 game1)
        {
            game = game1;
            sounds = new Dictionary<string, SoundEffectInstance>();
            songs = new Dictionary<string,Song>();
        }

        public SoundEffectInstance GetSound(string soundName)
        {
            return sounds[soundName];
        }

        public Song GetSong(string songName)
        {
            return songs[songName];
        }

        public void PlaySound(string soundName, float volume=0.5f)
        {
            if (!sounds.ContainsKey(soundName))
            {
                SoundEffect sound = game.Content.Load<SoundEffect>(Constants.SOUND_BASE_PATH+soundName);
                sounds.Add(soundName, sound.CreateInstance());
            }
            sounds[soundName].Volume = volume;
            sounds[soundName].Play();
        }

        public void PlaySong(string songName, float volume = 0.5f)
        {
            MediaPlayer.IsRepeating = true;
            if (!songs.ContainsKey(songName))
            {
                Song song = game.Content.Load<Song>(Constants.SONG_BASE_PATH+songName);
                songs.Add(songName, song);
            }
            MediaPlayer.Volume = volume;
            MediaPlayer.Play(songs[songName]);
        }

        public void StopSong()
        {
            MediaPlayer.Stop();
        }

        public void PauseSong()
        {
            MediaPlayer.Pause();
        }

        public void ResumeSong()
        {
            MediaPlayer.Resume();
        }

        public void StopSound(string soundName)
        {
            sounds[soundName].Stop();
        }

        public void PauseSound(string soundName)
        {
            sounds[soundName].Pause();
        }

        public void ResumeSound(string soundName)
        {
            sounds[soundName].Resume();
        }

        public bool IsSongPlaying()
        {
            return MediaPlayer.State == MediaState.Playing;
        }

        public bool IsSoundPlaying(string soundName)
        {
            if (!sounds.ContainsKey(soundName))
            {
                SoundEffect sound = game.Content.Load<SoundEffect>(Constants.SOUND_BASE_PATH + soundName);
                sounds.Add(soundName, sound.CreateInstance());
            }
            return sounds[soundName].State == SoundState.Playing;
        }
    }
}
