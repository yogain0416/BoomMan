namespace App.Initialization
{
    using System.Collections;
    using System.Threading.Tasks;
    using GoogleSheet;
    using UGS;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class Initialization : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        void Start()
        {
            // TODO 처음인지 아닌지 확인 후
            // 처음이면 파일 생성 해주고
            // 처음이 아니면 버전 확인해서 관련 DB들 업데이트 및 그대로 하기

            // 로컬데이터 불러오기
            UnityGoogleSheet.LoadAllData();
            SceneManager.LoadSceneAsync("Stage1");
            // StartCoroutine(CheckForUpdates());
        }

        private IEnumerator CheckForUpdates()
        {
            var loadTask = LoadSheetVersionAsync();
            yield return new WaitUntil(() => loadTask.IsCompleted);

            float sheetVersion = loadTask.Result;

            // 버전 같을 때
            if (BoomManData.Version.VersionList[0].version == sheetVersion) Debug.Log("버전이 같아요");
            // 버전 다를 때
            else
            {
                var generateTask = GenerateAllSheetsAsync();
                yield return new WaitUntil(() => generateTask.IsCompleted);
            }

            SceneManager.LoadSceneAsync("Stage1");
        }

        public async Task<float> LoadSheetVersionAsync()
        {
            TaskCompletionSource<float> tcs = new TaskCompletionSource<float>();

            UnityGoogleSheet.LoadFromGoogle<float, BoomManData.Version>((list, map) =>
            {
                float sheetVersion = list[0].version;
                tcs.SetResult(sheetVersion);
                Debug.Log("sheetVersion : " + sheetVersion);
            });

            // sheetVersion 값을 반환하기까지 기다림
            return await tcs.Task;
        }

        public async Task GenerateAsync<T>() where T : ITable
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            // UnityGoogleSheet의 Generate 호출
            UnityGoogleSheet.Generate<T>(true, true);

            // 약간의 지연을 두어 Generate 완료를 확인 (콜백이 없을 경우에 필요)
            await Task.Delay(500); // 필요에 따라 조정 가능

            tcs.SetResult(true);
            await tcs.Task;
        }

        public async Task GenerateAllSheetsAsync()
        {
            // 모든 Generate 작업을 비동기적으로 실행
            Task[] generateTasks = new Task[]
            {
                GenerateAsync<BoomManData.Version>(),
                GenerateAsync<BoomManData.Define>(),
                GenerateAsync<BoomManData.Character>(),
                GenerateAsync<BoomManData.PlayerStatus>(),
                GenerateAsync<BoomManData.OreStatus>(),
                GenerateAsync<BoomManData.Upgrade>()
            };

            // 모든 Task들이 완료될 때까지 대기
            await Task.WhenAll(generateTasks);

            Debug.Log("모든 Google Sheet 데이터 생성 완료");
        }


        // TODO 업데이트 로딩 UI부분
    }
}
