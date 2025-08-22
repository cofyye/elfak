export interface IFirstName {
    firstName: string;
}

export interface ILastName {
    lastName: string;
}

export interface IUsername {
    username: string;
}

export interface IPassword {
    currentPassword: string;
    password: string;
    confirmPassword: string;
}

export interface IProfession {
    bio: string;
    profession: string;
}

export interface IAvatar {
    avatar: string;
}

export interface IProfileGet extends IFirstName, ILastName, IUsername, IProfession, IAvatar {

}
