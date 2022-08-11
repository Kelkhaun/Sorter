using UnityEngine;
using System.Collections.Generic;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] MoneyImage _image1;
    [SerializeField] MoneyImage _image2;
    [SerializeField] Vagon[] _vagons;
    [SerializeField] private List<Transform> _points;

    private void OnEnable()
    {
        for (int i = 0; i < _vagons.Length; i++)
        {
            _vagons[i].PassengerEnteredTheVagon += ShowMoney;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _vagons.Length; i++)
        {
            _vagons[i].PassengerEnteredTheVagon -= ShowMoney;
        }
    }

    private void ShowMoney(int passangersNumberInVagon, int vagonCapacity, Passenger passenger, Vagon vagon)
    {
        if (vagon.IsBlue == true && passenger.IsBlue == true)
        {
            ShowSprite(_image1);
        }
        else if (vagon.IsBlue == true && passenger.IsRed == true)
        {
            ShowSprite(_image2);
        }
        else if (vagon.IsRed == true && passenger.IsRed == true)
        {
            ShowSprite(_image1);
        }
        else if (vagon.IsRed && passenger.IsBlue == true)
        {
            ShowSprite(_image2);
        }
    }

    private void ShowSprite(MoneyImage image)
    {
        int minimumValue = 0;
        int spawnPoint = Random.Range(minimumValue, _points.Count);
        Instantiate(image, _points[spawnPoint].position, transform.rotation, transform);
    }

}
