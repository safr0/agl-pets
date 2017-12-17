import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { LoadingModule } from 'ngx-loading';


import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { PetsComponent } from './components/pets/pets.component';

import { PetsService } from './components/pets/pets.service';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,        
        PetsComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        LoadingModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'fetch-pets', pathMatch: 'full' },
            { path: 'fetch-pets', component: PetsComponent },
            { path: '**', redirectTo: 'fetch-pets' }
        ])
    ],
    providers: [
        PetsService
    ]
})
export class AppModuleShared {
}
