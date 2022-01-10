using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Photon.Pun;
namespace YoutubePlayer
{
    public class VideoControl : MonoBehaviourPunCallbacks
    {
        public YoutubePlayer youtubePlayer;

        VideoPlayer videoPlayer;

        public GameObject quad;
        public Button playBtn;
        public Button pauseBtn;
        public Button stopBtn;
        public Button showHideBtn;

        PhotonView m_photonView;

        public void Awake()
        {
            m_photonView = quad.GetComponent<PhotonView>();

            playBtn.interactable = false; 
            pauseBtn.interactable = false;
            stopBtn.interactable = false;

            videoPlayer = youtubePlayer.GetComponent<VideoPlayer>();
            videoPlayer.prepareCompleted += VideoPlayerPreparedCompleted;
        }

        void VideoPlayerPreparedCompleted(VideoPlayer source)
        {
            playBtn.interactable = source.isPrepared;
            pauseBtn.interactable = source.isPrepared;
            stopBtn.interactable = source.isPrepared;
        }

        [PunRPC]
        public async void Prepare()
        {
            print("loading video....");
            try
            {
                await youtubePlayer.PrepareVideoAsync();
                print("video loaded");
            }
            catch
            {
                print("ERROR loading video");
            }
        }

        [PunRPC]
        public void PlayVideo()
        {
            videoPlayer.Play();
        }

        [PunRPC]
        public void StopVideo()
        {
            videoPlayer.Stop();
            videoPlayer.Play();
        }

        [PunRPC]
        public void PauseVideo()
        {
            videoPlayer.Pause();
        }

        private void OnDestroy()
        {
            videoPlayer.prepareCompleted -= VideoPlayerPreparedCompleted;
        }

        [PunRPC]
        public void ShowHide()
        {
            if(quad.activeSelf== true)
            {
                quad.SetActive(false);
            } else
            {
                quad.SetActive(true);
            }
        }

        public void ShowHideRPC()
        {
            photonView.RPC("ShowHide", RpcTarget.AllBufferedViaServer);
        }

        public void PlayRPC()
        {
            photonView.RPC("PlayVideo", RpcTarget.AllBufferedViaServer);
        }

        public void PauseRPC()
        {
            photonView.RPC("PauseVideo", RpcTarget.AllBufferedViaServer);
        }

        public void StopRPC()
        {
            photonView.RPC("StopVideo", RpcTarget.AllBufferedViaServer);
        }

        public void PrepareRPC()
        {
            photonView.RPC("Prepare", RpcTarget.AllBufferedViaServer);
        }
    }
}


