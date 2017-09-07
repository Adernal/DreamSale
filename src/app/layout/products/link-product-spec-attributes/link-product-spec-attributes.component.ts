import { Component, OnInit, ViewChild, Input, ElementRef } from '@angular/core';
import { NgForm  } from '@angular/forms';
import { Http } from '@angular/http';
import { LinkProductSpecAttributesService } from './link-product-spec-attributes.service';
import { ProductAttributesService } from '../product-attributes/product-attributes.service';


@Component({
    selector: 'app-link-product-spec-attributes',
    templateUrl: './link-product-spec-attributes.component.html',
    styleUrls: ['./link-product-spec-attributes.component.scss']
})
export class LinkProductSpecAttributesComponent implements OnInit {
    @Input('ProductId') Id;


    loadingImagePath: string;
    @ViewChild('s') specAttributeForm: NgForm;
    loadingSpecAttributes: boolean;
    specAttributeList = [];
    specAttributeId: number;
    ValueRaw: string;
    currentSpecAttribute = [];
    specAttributeName: string;
    AttributeId: number;
    product_attributes;
    constructor(private http: Http, private specAttributeService: LinkProductSpecAttributesService , private productAttributeService : ProductAttributesService) {
        this.loadingSpecAttributes = false;
        this.specAttributeId = 0;
        this.ValueRaw = '';
        this.specAttributeName = '';
        this.loadingImagePath = '../../../assets/images/ajax-loader.gif';
        this.AttributeId = 0;
    }

    ngOnInit() {
    }
    getCurrentSpecAttributes() {

        this.loadingSpecAttributes = true;
        this.specAttributeService.getCurrentSpecAttributes(this.Id)
            .subscribe(
            (response) => {
                this.specAttributeList = (response.json().Data);
                console.log(response.json());

                this.loadingSpecAttributes = false;
            },
            (error) => {
                console.log(error);
                alert("Can't fetch data ! Please refresh or check your connnection !");
            }
            );
    }
    addSpecAttribute() {
        this.loadingSpecAttributes = true;
        this.AttributeId = this.specAttributeForm.value.current_attribute_id;
        this.specAttributeId = this.specAttributeForm.value.current_spec_attribute_id;
        this.specAttributeName = this.getCurrentSpecAttributeName(this.specAttributeId)[0].Name;
        this.ValueRaw = this.specAttributeForm.value.ValueRaw;
        this.specAttributeService.addSpecAttribute(this.Id, this.AttributeId, this.specAttributeId, this.specAttributeName, this.ValueRaw)
            .subscribe(
            (data) => {

                alert('Added !');
                this.specAttributeForm.reset();
                this.loadingSpecAttributes=false;
                this.getCurrentSpecAttributes();
            },
            (error) => {
                console.log(error)
                alert('Can\'t fetch data ! Please refresh or check your connnection !')
            }
            );

    }
    getCurrentSpecAttributeName(id: number) {
        return this.specAttributeList.filter(
            function(attribute) { return attribute.Id == id }
        );
    }
    deleteCurrentSpecAttribute(id: HTMLFormElement) {
        var confirmation = confirm("Are you sure you want to delete ?");
        if (confirmation) {
            this.loadingSpecAttributes = true;
            this.specAttributeService.deleteSpecAttribute(+id.name)
                .subscribe(
                (data) => {

                    alert('Deleted !');

                    this.specAttributeForm.reset();
                    this.loadingSpecAttributes = false;
                    this.getCurrentSpecAttributes();
                },
                (error) => {
                    console.log(error)
                    alert('Can\'t fetch data ! Please refresh or check your connnection !')
                }
                );
        }
    }
    getAttributes(){
        this.productAttributeService.getAttributes()
            .subscribe(
            (response) => {
                this.product_attributes = (response.json().Data);

            },
            (error) =>      {
                    console.log(error);
                    alert("Can't fetch data ! Please refresh or check your connnection !");
                  }
            );

    }
}