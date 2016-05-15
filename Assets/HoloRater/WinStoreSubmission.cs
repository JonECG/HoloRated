using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace HoloRater
{ 
    public class WinStoreSubmission : RatingSubmissionHandler {
                
        public override void SubmitRating(RatingWindow window, RatingWidget widget)
        {
#if WINDOWS_UWP
            var uri = new System.Uri("ms-windows-store:reviewapp?appid=9WZDNCRFJ140");
            var op = Windows.System.Launcher.LaunchUriAsync(uri);
            StartCoroutine(WaitForLauncher(window, op));
#else
            window.ProcessSubmissionResults(new SubmissionResult { Succeeded = false, ErrorMessage = "Cannot open Windows Store on when not in a UWP application" });
#endif
        }

#if WINDOWS_UWP
        IEnumerator WaitForLauncher(RatingWindow window, Windows.Foundation.IAsyncOperation<bool> op )
        {
            float startTime = Time.time;
            while ( op.Status == Windows.Foundation.AsyncStatus.Started && (Time.time - startTime) < 5 )
            {
                yield return null;
            }

            SubmissionResult result = new SubmissionResult();

            if(  op.Status == Windows.Foundation.AsyncStatus.Started )
            {
                result.Succeeded = false;
                result.ErrorMessage = "Request timed out";
            }
            else
            {
                result.Succeeded = op.Status == Windows.Foundation.AsyncStatus.Completed;
                result.ErrorMessage = string.Format( "Operation {0}, code [{1}]", op.Status == Windows.Foundation.AsyncStatus.Error ? "ended in error" : "cancelled", op.ErrorCode);
            }

            window.ProcessSubmissionResults(result);
        }
#endif
    }

}
