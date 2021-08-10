using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;
using System;

public class TopPanelPresenter : MonoBehaviour
{
    [SerializeField] private Text _time;
    [SerializeField] private Button _menu;
    [SerializeField] private GameObject _menuObject;

    [Inject]
    private void Init(ITimeModel timeModel)
    {
        timeModel.GameTime.Subscribe(seconds =>
        {
            var t = TimeSpan.FromSeconds(seconds);
            _time.text = string.Format("{0:D2}:{1:D2}",
                        t.Minutes,
                        t.Seconds);
        });

        _menu.OnClickAsObservable().Subscribe(_ => _menuObject.SetActive(true));
    }
}