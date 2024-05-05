using System.Collections.Generic;
using Colors;
using Integration;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopService : MonoBehaviour
{
    [SerializeField] 
    private GameObject _shopDiamondsPanel;
    [SerializeField] 
    private GameObject _SkinPanel;
    [SerializeField] 
    private List<Button> _shopDiamondsPanelOpenButtons;
    [SerializeField] 
    private Button _diamondsPanelCloseButton;

    [SerializeField]
    private List<DiamondsHolder> _allDiamondInfoPanel;
    
    [SerializeField]
    private Button _buyPack1;
    [SerializeField]
    private Button _buyPack2;
    [SerializeField]
    private Button _buyPack3;
    [SerializeField]
    private Button _buyPack4;

    private IAPService _iapService;
    
    [Inject]
    private void Construct(IAPService iapService)
    {
        _iapService = iapService;
    }
    
    private void Awake()
    {
        foreach (var button in _shopDiamondsPanelOpenButtons)
        {
            button.onClick.AddListener(OpenShopDiamondsPanel);
        }
        _diamondsPanelCloseButton.onClick.AddListener(CloseShopDiamondsPanel);
        _buyPack1.onClick.AddListener(BuyPack1);
        _buyPack2.onClick.AddListener(BuyPack2);
        _buyPack3.onClick.AddListener(BuyPack3);
        _buyPack4.onClick.AddListener(BuyPack4);
        
        _iapService.OnUpdateDiamondsAmount += UpdateViewDiamonds;
    }

    private void OnDestroy()
    {
        foreach (var button in _shopDiamondsPanelOpenButtons)
        {
            button.onClick.RemoveListener(OpenShopDiamondsPanel); 
        }
        _diamondsPanelCloseButton.onClick.RemoveListener(CloseShopDiamondsPanel);
        _buyPack1.onClick.RemoveListener(BuyPack1);
        _buyPack2.onClick.RemoveListener(BuyPack2);
        _buyPack3.onClick.RemoveListener(BuyPack3);
        _buyPack4.onClick.RemoveListener(BuyPack4);
        
        _iapService.OnUpdateDiamondsAmount -= UpdateViewDiamonds;
    }
    
    private void OpenShopDiamondsPanel()
    {
        _shopDiamondsPanel.SetActive(true);
        _SkinPanel.SetActive(false);
    }
    private void CloseShopDiamondsPanel()
    {
        _SkinPanel.SetActive(true);
        _shopDiamondsPanel.SetActive(false);
    }

    private void UpdateViewDiamonds()
    {
        foreach (var diamondsHolder in _allDiamondInfoPanel)
        {
            diamondsHolder.UpdateDiamondsView();
        }
    }
    
    
    private void BuyPack1()
    {
        _iapService.BuyPack1();
    }
    private void BuyPack2()
    {
        _iapService.BuyPack2();
    }
    private void BuyPack3()
    {
        _iapService.BuyPack3();
    }
    private void BuyPack4()
    {
        _iapService.BuyPack4();
    }
}
