using System;
using System.Collections.Generic;
using System.IO;
using NAudio.Wave;



namespace P2DEngine.Engine
{
    public class P2DAudioManager // Esta es una clase para cargar y hacer escuchar SFX.
    {
        static private string AudioPath = "Assets/Audio/";
        static private Dictionary<string, MemoryStream> AudioMap = new Dictionary<string, MemoryStream>();

        static private int PlayingIndex = 0;
        static private Dictionary<int, WaveOutEvent> PlayingAudioMap = new Dictionary<int, WaveOutEvent>();


        public static void Load(string filaname, string id)
        {
            string pathname = AudioPath + filaname;
            if (File.Exists(pathname))
            {
                if (AudioMap.ContainsKey(id))
                {
                    throw new Exception(id);
                }
                else
                {
                    MemoryStream stream = new MemoryStream(File.ReadAllBytes(pathname));
                    AudioMap.Add(id, stream);
                }
            }
            else
            {
                throw new Exception(pathname);
            }
        }

        static public int Play(string id)
        {
            if (AudioMap.ContainsKey(id))
            {
                MemoryStream stream = AudioMap[id];
                StreamMediaFoundationReader reader = new StreamMediaFoundationReader(stream);
                WaveOutEvent player = new WaveOutEvent();
                player.Init(reader);
                player.Play();

                int index = PlayingIndex;
                PlayingIndex++;
                PlayingAudioMap.Add(index, player);

                return index;
            }

            throw new Exception(id);
        }

        static public void Stop(int playingId)
        {
            if (PlayingAudioMap.ContainsKey(playingId))
            {
                WaveOutEvent player = PlayingAudioMap[playingId];
                player.Stop();
                PlayingAudioMap.Remove(playingId);
            }
        }

        static public void Pause(int playingId)
        {
            if (PlayingAudioMap.ContainsKey(playingId))
            {
                WaveOutEvent player = PlayingAudioMap[playingId];
                player.Pause();
            }
        }

        static public void Resume(int playingId)
        {
            if (PlayingAudioMap.ContainsKey(playingId))
            {
                WaveOutEvent player = PlayingAudioMap[playingId];
                player.Play();
            }
        }
    }
}
