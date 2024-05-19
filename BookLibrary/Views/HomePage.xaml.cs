using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Views;

public partial class HomePage : ContentPage
{
    private bool _isScrolling;
    double x, y;
    public HomePage()
    {
        InitializeComponent();
    }

    private void ItemsView_OnScrolled(object? sender, ItemsViewScrolledEventArgs e)
    {
        _isScrolling = true;
        FadeOutButton();

        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            if (_isScrolling)
            {
                _isScrolling = false;
                return true;
            }
            else
            {
                FadeInButton();
                return false;
            }
        });    
    }
    
    private async void FadeOutButton()
    {
        await AddButton.FadeTo(0, 150);
    }

    private async void FadeInButton()
    {
        await AddButton.FadeTo(1, 150);
    }

    private void PanGestureRecognizer_OnPanUpdated(object? sender, PanUpdatedEventArgs e)
    {
        var button = sender as Frame;

        switch (e.StatusType)
        {
            case GestureStatus.Started:
                x = button.TranslationX;
                y = button.TranslationY;
                break;
            case GestureStatus.Running:
                button.TranslationX = x + e.TotalX;
                button.TranslationY = y + e.TotalY;
                break;
            case GestureStatus.Completed:
                x = button.TranslationX;
                y = button.TranslationY;
                break;
        }    
    }
    
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Reset the position of the button
        resetButtonLocation();
    }

    private void resetButtonLocation()
    {
        AddButton.TranslationX = 0;
        AddButton.TranslationY = 0;
    }

}