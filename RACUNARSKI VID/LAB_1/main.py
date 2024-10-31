import cv2
import numpy as np
import matplotlib.pyplot as plt

def show_image(image, title="Slika"):
    plt.imshow(image, cmap='gray')
    plt.title(title)
    plt.axis('off')
    plt.show()

try:
    # Putanja slike
    image_path = "./slika_4.png"

    # Otvaranje slike
    image = cv2.imread(image_path)

    # Provera da li je slika učitana
    if image is None:
        raise ValueError(f"Ne mogu otvoriti sliku: {image_path}")

    # Pretvaranje slike u nijanse sive
    image = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
    show_image(image, title="Ulazna slika")

    # Prebacivanje slike u frekventni domen
    fft_image = np.fft.fft2(image)
    fft_image = np.fft.fftshift(fft_image)

    # Nalazenje magnitude spektra
    magnitude_fft_image = np.log(np.abs(fft_image) + 1)  # Dodavanje 1 radi stabilnosti logaritma
    show_image(magnitude_fft_image, title="Magnituda spektra pre uklanjanja suma")

    # Filtriranje slike postavljanjem sumne tacke na 0
    noisy_indices = [(206, 206), (206, 306), (306, 206), (306, 306)]
    for xy in noisy_indices:
        fft_image[xy] = 0

    # Ponovni prikaz magnitude spektra nakon uklanjanja suma
    magnitude_fft_image_filtered = np.log(np.abs(fft_image) + 1)
    show_image(magnitude_fft_image_filtered, title="Magnituda spektra nakon uklanjanja suma")

    # Vracanje slike iz frekventnog u prostorni domen
    filtered_image = np.abs(np.fft.ifft2(np.fft.ifftshift(fft_image)))
    show_image(filtered_image, title="Ulazna slika nakon uklanjanja suma")

except ValueError as e:
    print(e)
    exit()
finally:
    pass
