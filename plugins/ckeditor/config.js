/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
    config.enterMode = CKEDITOR.ENTER_BR // pressing the ENTER Key puts the <br/> tag 
    config.filebrowserBrowseUrl = '../../plugins/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '../../plugins/ckfinder/ckfinder.html?type=Images';
    config.filebrowserFlashBrowseUrl = '../../plugins/ckfinder/ckfinder.html?type=Flash';
    config.filebrowserUploadUrl = '../../plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '../../plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '../../plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
};
