import cv2
import numpy as np
import matplotlib.pyplot as plt


def main():
    # Ucitavamo sliku
    image = cv2.imread('coins.png')
    image_rgb = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)  # BGR u RGB za plt prikaz

    # Pretvaramo sliku u nijansu sive
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)

    # Binarna slika pomocu praga
    _, thresh = cv2.threshold(gray, 150, 255, cv2.THRESH_BINARY_INV)

    # Zatvaranje (morfoloska operacija)
    kernel = cv2.getStructuringElement(cv2.MORPH_ELLIPSE, (15, 15))
    closing = cv2.morphologyEx(thresh, cv2.MORPH_CLOSE, kernel)

    # Extraktovanje saturacije
    hsv_image = cv2.cvtColor(image, cv2.COLOR_BGR2HSV)
    saturation = hsv_image[:, :, 1]

    # Marker pomocu praga
    _, marker = cv2.threshold(saturation, 30, 255, cv2.THRESH_BINARY)

    # Otvaranje i zatvaranje za poboljsanje markera:
    marker = cv2.morphologyEx(marker, cv2.MORPH_OPEN, kernel)
    reconstructed = cv2.morphologyEx(marker, cv2.MORPH_CLOSE, kernel)

    # Kombinovanje maski
    final_mask = cv2.bitwise_and(closing, reconstructed)

    # Prikaz originalne slike
    plt.imshow(image_rgb)
    plt.show()

    # Prikaz finalne maske u sivoj boji
    plt.imshow(final_mask, cmap='gray')
    plt.show()

    # Prikaz konacne slike gde je finalna maska primenjena na originalnu sliku
    result = cv2.bitwise_and(image, image, mask=final_mask)
    result_rgb = cv2.cvtColor(result, cv2.COLOR_BGR2RGB)
    plt.imshow(result_rgb)
    plt.show()


main()